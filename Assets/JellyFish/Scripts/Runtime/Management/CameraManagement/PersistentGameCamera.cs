/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
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
        private bool _primaryGameCamera = false;


        /// <summary>
        /// Indicate if the main game camera is registered.
        /// </summary>
        private bool _hasRegisteredCamera = false;

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
                GameCamera.Camera = MainGameCamera;

                DontDestroyOnLoad(gameObject);
                _hasRegisteredCamera = true;
                _primaryGameCamera = true;
            }
            else if (DontDestroy && !_primaryGameCamera)
            {
                Destroy(gameObject);
            }
            else
            {
                GameCamera.Camera = MainGameCamera;
            }
        }

        #endregion
    }
}