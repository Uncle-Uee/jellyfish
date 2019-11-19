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
namespace JellyFish.ObjectPool.Master
{
    /// <summary>
    /// The Master Pool Consists of a Dictionary with a Key = Int and a Value = List. This Master Pool only needs to be created once and can be used by multiple objects.
    /// </summary>
    [CreateAssetMenu(menuName = "Uee/Object Pool/Master Pool")]
    public class MasterPool : ObjectPoolBase
    {
        #region VARIABLES

        /// <summary>
        /// Master Object Pool.
        /// </summary>
        public Dictionary<int, List<GameObject>> Pool = new Dictionary<int, List<GameObject>>();

        #endregion


        #region MASTER POOL METHODS

        /// <summary>
        /// Create an Object Pool.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parent"></param>
        public override void CreateObjectPool(GameObject prefab, Transform parent)
        {
            int key = prefab.GetInstanceID();

            if (!Pool.ContainsKey(key))
            {
                List<GameObject> pool         = new List<GameObject>();
                GameObject       parentObject = new GameObject($"--- {prefab.name.ToUpper()} POOL ---");
                parentObject.transform.parent = parent;

                for (int i = 0; i < DefaultSize; i++)
                {
                    GameObject cachedGameObject = InstantiatePrefab(prefab, parentObject.transform);
                    SetReferences(cachedGameObject, key, pool, ActivePool);

                    pool.Add(cachedGameObject);
                }

                Pool.Add(key, pool);
            }
        }


        /// <summary>
        /// Get the First Available Object from the Pool.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public GameObject GetObject(int key)
        {
            if (!Pool.ContainsKey(key))
            {
                return null;
            }

            for (int i = Pool[key].Count - 1; i >= 0; i--)
            {
                GameObject cachedGameObject = Pool[key][i];

                if (!cachedGameObject.activeInHierarchy)
                {
                    cachedGameObject.SetActive(true);

                    if (GetIReference(cachedGameObject) != null)
                    {
                        Pool[key].RemoveAt(i);
                        ActivePool.Add(cachedGameObject);
                        Reference.Index = Mathf.Clamp(Reference.Index, 0, ActivePool.Count - 1);
                    }

                    return cachedGameObject;
                }
            }

            if (ExpandPool && ExpandSize > 0)
            {
                GameObject cachedGameObject = null;

                for (int i = 0; i < ExpandSize; i++)
                {
                    cachedGameObject = InstantiatePrefab(Pool[key][0], Pool[key][0].transform.parent);
                    SetReferences(cachedGameObject, key, Pool[key], ActivePool);
                    Pool[key].Add(cachedGameObject);
                }

                if (Reference != null)
                {
                    Pool[key].RemoveAt(Pool[key].Count - 1);
                    ActivePool.Add(cachedGameObject);
                    Reference.Index = Mathf.Clamp(Reference.Index, 0, ActivePool.Count - 1);
                }

                if (cachedGameObject != null)
                {
                    cachedGameObject.SetActive(true);

                    return cachedGameObject;
                }
            }

            return null;
        }

        /// <summary>
        /// Get the First Available Object from the Pool.
        /// </summary>
        public GameObject GetObject(GameObject prefab)
        {
            return GetObject(prefab.GetInstanceID());
        }

        /// <summary>
        /// Get the First Available Object from the Pool and Set its Position.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public GameObject GetObject(int key, Vector3 position)
        {
            GameObject cachedGameObject = GetObject(key);
            cachedGameObject.transform.position = position;

            return cachedGameObject;
        }

        /// <summary>
        /// Get the First Available Object from the Pool and Set its Position.
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public GameObject GetObject(GameObject prefab, Vector3 position)
        {
            return GetObject(prefab.GetInstanceID(), position);
        }

        #endregion
    }
}