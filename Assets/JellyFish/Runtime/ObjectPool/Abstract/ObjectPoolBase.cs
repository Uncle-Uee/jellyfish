/*
 *
 * Created By: Ubaidullah Effendi-Emjedi, Uee
 * Alias: Uee
 * Modified By:
 *
 * Last Modified: 09 November 2019
 *
 */

using System.Collections.Generic;
using JellyFish.ObjectPool.Interfaces;
using JellyFish.Data.Primitive;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.ObjectPool.Abstract
{
    /// <summary>
    /// Object Pool Base Class.
    /// </summary>
    public abstract class ObjectPoolBase : ScriptableObject
    {
        #region VARIABLES

        /// <summary>
        /// Default 'x' Size of Pool.
        /// </summary>
        [Header("Object Pool Settings")]
        public IntReference DefaultSize;

        /// <summary>
        /// Expand the Object Pool.
        /// </summary>
        public bool ExpandPool = true;

        /// <summary>
        /// Expand the Pool by 'x' Objects.
        /// </summary>
        public IntReference ExpandSize;


        /// <summary>
        /// Active List of GameObjects.
        /// </summary>
        protected readonly List<GameObject> ActivePool = new List<GameObject>();

        /// <summary>
        /// Pool References.
        /// </summary>
        protected IReference Reference;

        #endregion


        #region OBJECT POOL BASE METHODS

        /// <summary>
        /// Create an Object Pool.
        /// </summary>
        /// <param name="parent"></param>
        public virtual void CreateObjectPool(Transform parent)
        {
        }

        /// <summary>
        /// Create an Object Pool.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        public virtual void CreateObjectPool(GameObject prefab, Transform parent)
        {
        }

        /// <summary>
        /// Instantiate a Prefab.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public GameObject InstantiatePrefab(GameObject prefab, Transform parent, bool isActive = false)
        {
            GameObject cachedGameObject = Instantiate(prefab, parent);
            cachedGameObject.SetActive(isActive);

            return cachedGameObject;
        }

        /// <summary>
        /// Get IReference Component
        /// </summary>
        /// <returns></returns>
        public IReference GetIReference(GameObject gameObject)
        {
            return gameObject.GetComponent<IReference>();
        }

        /// <summary>
        /// Set the Pool Reference.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="pool"></param>
        /// <param name="activePool"></param>
        public void SetPoolReference(GameObject gameObject, List<GameObject> pool, List<GameObject> activePool)
        {
            Reference = GetIReference(gameObject);

            if (Reference != null)
            {
                Reference.Pool       = pool;
                Reference.ActivePool = activePool;
            }
        }

        /// <summary>
        /// Set the ID Reference.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="id"></param>
        public void SetIDReference(GameObject gameObject, int id)
        {
            Reference    = GetIReference(gameObject);
            Reference.ID = Reference != null ? id : -1;
        }

        /// <summary>
        /// Set References to the Pool and Prefab Instance ID.
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="gameObject"></param>
        /// <param name="id"></param>
        /// <param name="activePool"></param>
        public void SetReferences(GameObject gameObject, int id, List<GameObject> pool, List<GameObject> activePool)
        {
            Reference = GetIReference(gameObject);

            if (Reference != null)
            {
                Reference.ID         = id;
                Reference.Pool       = pool;
                Reference.ActivePool = activePool;
            }
        }

        #endregion
    }
}