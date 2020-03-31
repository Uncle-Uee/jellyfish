// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System;
using System.Collections;
using System.Collections.Generic;
using SOFlow.Data.Primitives;
using UnityEngine;

namespace JellyFish.ObjectPooling
{
    public abstract class ObjectPoolBase : ScriptableObject
    {
        /// <summary>
        ///     Gets the object pool enumerable of this instance.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable GetPool();

        /// <summary>
        /// The current size of the pool.
        /// </summary>
        public int CurrentPoolSize { get; protected set; }

        public void OnEnable()
        {
            CurrentPoolSize = 0;
        }
    }

    public class ObjectPool<T> : ObjectPoolBase where T : IPoolObject<T>
    {
        /// <summary>
        ///     The pool.
        /// </summary>
        public Dictionary<string, List<T>> Pool = new Dictionary<string, List<T>>();

        /// <summary>
        ///     The list of available objects.
        /// </summary>
        public List<T> AvailableObjects = new List<T>();

        /// <summary>
        ///     The initial object pool size per object ID.
        /// </summary>
        public IntField InitialPoolSizePerObject;

        /// <summary>
        ///     The amount of object to instantiate when the pool runs out.
        /// </summary>
        public IntField PoolSizeExtensionAmount;

        /// <summary>
        /// The pool container.
        /// </summary>
        private Transform _poolContainer;

        /// <inheritdoc />
        public override IEnumerable GetPool()
        {
            return Pool;
        }

        /// <summary>
        ///     Initializes the pool.
        /// </summary>
        /// <param name="poolContainer"></param>
        public void InitializePool(Transform poolContainer)
        {
            _poolContainer = poolContainer;
            Pool.Clear();

            foreach (T _object in AvailableObjects)
            {
                if (!Pool.ContainsKey(_object.ID))
                    Pool.Add(_object.ID, new List<T>(InitialPoolSizePerObject));

                for (int i = 0; i < InitialPoolSizePerObject; i++)
                {
                    T generatedObject = _object.Instantiate(_poolContainer);
                    generatedObject.DeactivateObject();
                    Pool[_object.ID].Add(generatedObject);
                }

                CurrentPoolSize += InitialPoolSizePerObject;
            }
        }

        /// <summary>
        ///     Generates a list of objects for the provided object ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<T> GenerateObjects(string id)
        {
            T objectToGenerate = AvailableObjects.Find(_object => _object.ID == id);

            if (objectToGenerate == null)
            {
                Debug.LogWarning($"Object with ID [{id}] not available in pool.");

                return null;
            }

            List<T> generatedObjects = new List<T>(PoolSizeExtensionAmount);

            for (int i = 0; i < PoolSizeExtensionAmount; i++)
            {
                T generatedObject = objectToGenerate.Instantiate(_poolContainer);
                generatedObject.DeactivateObject();
                generatedObjects.Add(generatedObject);
            }

            CurrentPoolSize += PoolSizeExtensionAmount;

            return generatedObjects;
        }

        /// <summary>
        ///     Gets an object from the pool with the provided ID.
        /// </summary>
        /// <param name="objectReference"></param>
        /// <returns></returns>
        public T GetObjectFromPool(T objectReference)
        {
            return GetObjectFromPool(objectReference.ID);
        }

        /// <summary>
        ///     Gets an object from the pool with the provided ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoActivateid"></param>
        /// <returns></returns>
        public T GetObjectFromPool(string id, bool autoActivate = true)
        {
            T _object = default;
            List<T> poolObjects;

            if (Pool.TryGetValue(id, out poolObjects))
            {
                if (poolObjects.Count <= 0)
                {
                    List<T> generatedObjects = GenerateObjects(id);

                    if (generatedObjects != null)
                    {
                        poolObjects.AddRange(generatedObjects);

                        _object = poolObjects[0];
                        poolObjects.RemoveAt(0);
                    }
                }
                else
                {
                    _object = poolObjects[0];
                    poolObjects.RemoveAt(0);
                }
            }
            else
            {
                List<T> newPoolObject = new List<T>();
                Pool.Add(id, newPoolObject);

                List<T> generatedObjects = GenerateObjects(id);

                if (generatedObjects != null)
                {
                    newPoolObject.AddRange(generatedObjects);

                    _object = newPoolObject[0];
                    newPoolObject.RemoveAt(0);
                }
            }

            if (autoActivate && _object != null)
            {
                _object.ActivateObject();
                CurrentPoolSize--;
            }

            return _object;
        }

        /// <summary>
        /// Gets a list of objects from the object pool with the given ID.
        /// </summary>
        /// <param name="objectReference"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public List<T> GetObjectsFromPool(T objectReference, int amount)
        {
            return GetObjectsFromPool(objectReference.ID, amount);
        }

        /// <summary>
        /// Gets a list of objects from the object pool with the given ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public List<T> GetObjectsFromPool(string id, int amount)
        {
            List<T> poolObjects = new List<T>(amount);

            for (int i = 0; i < amount; i++)
            {
                poolObjects.Add(GetObjectFromPool(id));
            }

            return poolObjects;
        }

        /// <summary>
        ///     Returns the given object to the pool.
        /// </summary>
        /// <param name="_object"></param>
        public void ReturnObjectToPool(T _object)
        {
            if (_object == null)
            {
                return;
            }

            _object.DeactivateObject();
            List<T> poolObjects;

            if (Pool.TryGetValue(_object.ID, out poolObjects))
            {
                if (!poolObjects.Contains(_object))
                {
                    poolObjects.Add(_object);
                    CurrentPoolSize++;
                }
            }
            else
            {
                List<T> newPoolObjects = new List<T>();
                Pool.Add(_object.ID, newPoolObjects);

                newPoolObjects.Add(_object);
                CurrentPoolSize++;
            }
        }

        /// <summary>
        ///     Returns the given object to the pool.
        /// </summary>
        /// <param name="_object"></param>
        public void ReturnObjectToPool(IPoolObjectRoot _object)
        {
            T convertedObject = (T) _object;

            if (convertedObject != null)
            {
                ReturnObjectToPool(convertedObject);
            }
        }

        /// <summary>
        /// Returns the given list of objects to the pool.
        /// </summary>
        /// <param name="objects"></param>
        public void ReturnObjectsToPool(List<T> objects)
        {
            foreach (T _object in objects)
            {
                ReturnObjectToPool(_object);
            }
        }

        /// <summary>
        /// Returns the given list of objects to the pool.
        /// </summary>
        /// <param name="objects"></param>
        public void ReturnObjectsToPool(List<IPoolObjectRoot> objects)
        {
            foreach (IPoolObjectRoot _object in objects)
            {
                ReturnObjectToPool(_object);
            }
        }
    }
}