using JellyFish.ObjectPool.Single;
using UnityEngine;

namespace Ship.Shooting
{
    public class Shooting1 : MonoBehaviour
    {
        #region VARIABLES

        /// <summary>
        /// Object Pool.
        /// </summary>
        [Header("Required Object Pool")]
        public SinglePool ObjectPool;


        [Header("Object ObjectPool Settings")]
        public Transform Parent;


        /// <summary>
        /// Rate of Fire.
        /// </summary>
        [Header("Shooting Settings")]
        public float FireRate;

        /// <summary>
        /// Automatically Fire Weapon.
        /// </summary>
        public bool AutoFire = false;


        /// <summary>
        /// Elapsed Time Before next Shot is Fired.
        /// </summary>
        private float _elapsedTime;

        #endregion


        #region UNITY METHODS

        private void Awake()
        {
            ObjectPool.CreateObjectPool(transform);
        }

        private void Update()
        {
            if (Input.GetButton("Fire1") && !AutoFire)
            {
                GetObjectFromPool();
            }
            else if (AutoFire)
            {
                GetObjectFromPool();
            }
        }

        private void OnDisable()
        {
            ObjectPool.Pool.Clear();
        }

        #endregion


        #region SHOOTING METHODS

        /// <summary>
        /// Get Object From ObjectPool.
        /// </summary>
        private void GetObjectFromPool()
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= FireRate)
            {
                // ToDo : When using Object ObjectPool, instead of Instantiating a new Object, Get the next Available from the ObjectPool!
                ObjectPool.GetObject(transform.position);
                _elapsedTime = 0;
            }
        }

        #endregion
    }
}