/*
 *
 * Created By: Ubaidullah Effendi-Emjedi, Uee
 * Alias: Uee
 * Modified By:
 *
 * Last Modified: 09 November 2019
 *
 */

using JellyFish.Data.Primitive;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Management.CameraManagement
{
    public class PersistentGameCamera : MonoBehaviour
    {
        #region VARIABLES

        /// <summary>
        /// Don't Destroy this Object On Load.
        /// </summary>
        [Header("Don't Destroy On Load")]
        public BooleanReference DontDestroy;

        /// <summary>
        /// Main Game Camera Reference.
        /// </summary>
        [Header("Camera References")]
        public Camera MainGameCamera;

        /// <summary>
        /// Game Camera Reference
        /// </summary>
        public CameraReference GameCamera;


        /// <summary>
        /// Indicates if this is the Primary Game Camera.
        /// </summary>
        private bool _primaryGameCamera;


        /// <summary>
        /// Indicate if the main game camera is registered.
        /// </summary>
        private bool _hasRegisteredCamera;

        #endregion

        #region UNITY METHODS

        public void Awake()
        {
            if (!MainGameCamera)
            {
                MainGameCamera = GetComponent<Camera>();
            }

            if (DontDestroy && !_hasRegisteredCamera)
            {
                GameCamera.Value = MainGameCamera;
                DontDestroyOnLoad(gameObject);
                _hasRegisteredCamera = true;
                _primaryGameCamera   = true;
            }
            else if (DontDestroy && !_primaryGameCamera)
            {
                Destroy(gameObject);
            }
            else
            {
                GameCamera.Value = MainGameCamera;
            }
        }

        #endregion
    }
}