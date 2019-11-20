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
using JellyFish.ObjectPool.Abstract;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.ObjectPool.Single
{
    [CreateAssetMenu(menuName = "JellyFish/Object Pool/Single Pool")]
    public class SinglePool : ObjectPoolBase
    {
        #region VARIABLES

        /// <summary>
        /// Prefab Reference.
        /// </summary>
        [Header("Prefab")]
        public GameObject Prefab;


        /// <summary>
        /// Object Pool.
        /// </summary>
        [Header("Object Pool")]
        public List<GameObject> Pool = new List<GameObject>();

        #endregion


        #region PROPERTIES

        #endregion


        #region SINGLE POOL METHODS

        /// <summary>
        /// Create an Object Pool.
        /// </summary>
        /// <param name="parent"></param>
        public override void CreateObjectPool(Transform parent)
        {
            GameObject parentObject = new GameObject($"--- {Prefab.name.ToUpper()} POOL ---");
            parentObject.transform.parent = parent;

            for (int i = 0; i < DefaultSize; i++)
            {
                GameObject cachedGameObject = InstantiatePrefab(Prefab, parentObject.transform);
                SetPoolReference(cachedGameObject, Pool, ActivePool);
                Pool.Add(cachedGameObject);
            }
        }


        /// <summary>
        /// Get the First Available Object from the Pool.
        /// </summary>
        /// <returns></returns>
        public GameObject GetObject()
        {
            for (int i = Pool.Count - 1; i >= 0; i--)
            {
                GameObject cachedGameObject = Pool[i];

                if (!cachedGameObject.activeInHierarchy)
                {
                    cachedGameObject.SetActive(true);

                    if (GetIReference(cachedGameObject) != null)
                    {
                        Pool.RemoveAt(i);
                        ActivePool.Add(cachedGameObject);
                        Reference.Index = Mathf.Clamp(Reference.Index, 0, ActivePool.Count - 1);
                    }

                    return cachedGameObject;
                }
            }

            if (ExpandPool)
            {
                GameObject cachedGameObject = null;

                for (int i = 0; i < ExpandSize; i++)
                {
                    cachedGameObject = InstantiatePrefab(Prefab, Pool[0].transform.parent);
                    SetPoolReference(cachedGameObject, Pool, ActivePool);
                    Pool.Add(cachedGameObject);
                }

                if (Reference != null)
                {
                    Pool.RemoveAt(Pool.Count - 1);
                    ActivePool.Add(cachedGameObject);
                    Reference.Index = Mathf.Clamp(Reference.Index, 0, ActivePool.Count - 1);
                }

                cachedGameObject.SetActive(true);

                return cachedGameObject;
            }

            return null;
        }

        /// <summary>
        /// Get the First Available GameObject and Set its Position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public GameObject GetObject(Vector3 position)
        {
            GameObject cachedGameObject = GetObject();
            cachedGameObject.transform.position = position;

            return cachedGameObject;
        }

        #endregion
    }
}