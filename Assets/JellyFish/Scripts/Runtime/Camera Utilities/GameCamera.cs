// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using UnityEngine;

namespace JellyFish.CameraUtilities
{
    [RequireComponent(typeof(Camera))]
    public class GameCamera : MonoBehaviour
    {
        /// <summary>
        ///     Indicates whether the game camera has already been registered.
        /// </summary>
        private static bool _hasRegisteredGameCamera;

        /// <summary>
        ///     Indicates whether this is the primary game camera.
        /// </summary>
        private bool _primaryGameCamera;

        /// <summary>
        ///     The game camera reference.
        /// </summary>
        public CameraReference GameCameraReference;

        /// <summary>
        ///     The scene game camera reference.
        /// </summary>
        public Camera SceneCameraReference;

        /// <summary>
        /// Do not Destroy Instance.
        /// </summary>
        public bool DontDestroy = false;

        /// <summary>
        ///     Registers the game camera.
        /// </summary>
        public void Start()
        {
            if (!_hasRegisteredGameCamera)
            {
                GameCameraReference.Camera = SceneCameraReference;

                if (DontDestroy)
                {
                    DontDestroyOnLoad(gameObject.ParentGameObject());
                }

                _hasRegisteredGameCamera = true;
                _primaryGameCamera = true;
            }
            else
            {
                if (!_primaryGameCamera && !DontDestroy)
                {
                    Destroy(gameObject);
                }
            }
        }

#if UNITY_EDITOR
        /// <summary>
        ///     Adds a Game Camera to the scene.
        /// </summary>
        [UnityEditor.MenuItem("GameObject/SOFlow/Camera Utilities/Add Game Camera", false, 10)]
        public static void AddComponentToScene()
        {
            Camera camera = UnityEditor.Selection.activeGameObject?.GetComponent<Camera>();

            if (camera != null)
            {
                GameCamera gameCamera = camera.gameObject.AddComponent<GameCamera>();
                gameCamera.SceneCameraReference = camera;

                return;
            }

            GameObject _gameObject = new GameObject("Game Camera", typeof(GameCamera));

            if (UnityEditor.Selection.activeTransform != null)
            {
                _gameObject.transform.SetParent(UnityEditor.Selection.activeTransform);
            }

            UnityEditor.Selection.activeGameObject = _gameObject;
        }
#endif
    }
}