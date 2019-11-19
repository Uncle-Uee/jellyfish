using System.Collections.Generic;
using JellyFish.ObjectPool.Interfaces;
using UnityEngine;

namespace Bullet.Movement
{
    public class Movement : MonoBehaviour, IReference
    {
        #region VARIABLES

        /// <summary>
        /// Speed of the Projectile.
        /// </summary>
        [Header("Movement Field")]
        public float Speed;

        /// <summary>
        /// Life Time of the Projectile.
        /// </summary>
        [Header("Life Time")]
        public float LifeTime;


        /// <summary>
        /// Elapsed Time.
        /// </summary>
        private float _elapsedTime = 0f;

        #endregion


        #region PROPERTIES

        public List<GameObject> Pool       { get; set; }
        public List<GameObject> ActivePool { get; set; }
        public int              Index      { get; set; }
        public int              ID         { get; set; }

        #endregion


        #region UNITY METHODS

        void Update()
        {
            _elapsedTime       += Time.deltaTime;
            transform.position += _elapsedTime * Speed * Vector3.right;

            if (_elapsedTime >= LifeTime)
            {
                // ToDo : When using Object Pool, Return to Pool instead of Destroying!
                gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            _elapsedTime       = 0;
            transform.position = Vector3.zero;
            ActivePool.RemoveAt(Index);
            Pool.Add(gameObject);
        }

        #endregion
    }
}