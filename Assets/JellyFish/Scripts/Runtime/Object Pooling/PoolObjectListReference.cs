// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using System.Collections.Generic;
using UnityEngine;

namespace JellyFish.ObjectPooling
{
    [CreateAssetMenu(menuName = "JellyFish/Object Pooling/Pool Object List Reference")]
    public class PoolObjectListReference : ScriptableObject
    {
        /// <summary>
        /// The pool object list reference.
        /// </summary>
        public List<IPoolObjectRoot> PoolObjects = new List<IPoolObjectRoot>();

        /// <summary>
        /// Indicates whether any pool objects are available in the list.
        /// </summary>
        public bool PoolObjectsAvailable => PoolObjectCount > 0;

        /// <summary>
        /// The cached list of popped objects.
        /// </summary>
        private List<IPoolObjectRoot> _poppedObjects = new List<IPoolObjectRoot>();

        /// <summary>
        /// The pool object count.
        /// </summary>
        public int PoolObjectCount { get; private set; }

        public void OnEnable()
        {
            PoolObjectCount = 0;
        }

        /// <summary>
        /// Adds a pool object to the list.
        /// </summary>
        /// <param name="poolObject"></param>
        public void Add(IPoolObjectRoot poolObject)
        {
            if (!PoolObjects.Contains(poolObject))
            {
                PoolObjects.Add(poolObject);
                PoolObjectCount++;
            }
        }

        /// <summary>
        /// Adds the given pool objects to the list.
        /// </summary>
        /// <param name="poolObjects"></param>
        public void Add(IEnumerable<IPoolObjectRoot> poolObjects)
        {
            foreach (IPoolObjectRoot poolObject in poolObjects)
            {
                Add(poolObject);
            }
        }

        /// <summary>
        /// Gets the last pool object from the list.
        /// </summary>
        /// <returns></returns>
        public IPoolObjectRoot PopLast()
        {
            if (PoolObjectCount > 0)
            {
                IPoolObjectRoot poolObject = PoolObjects[PoolObjectCount - 1];
                PoolObjectCount--;
                PoolObjects.RemoveAt(PoolObjectCount);

                return poolObject;
            }

            return null;
        }

        /// <summary>
        /// Gets all pool objects from the list.
        /// </summary>
        /// <returns></returns>
        public List<IPoolObjectRoot> PopAll()
        {
            _poppedObjects.Clear();
            _poppedObjects.AddRange(PoolObjects);

            PoolObjects.Clear();
            PoolObjectCount = 0;

            return _poppedObjects;
        }
    }
}