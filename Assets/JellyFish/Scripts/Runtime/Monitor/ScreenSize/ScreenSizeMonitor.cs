/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using JellyFish.Data.Primitive;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Monitor.ScreenSize
{
    public class ScreenSizeMonitor : MonoBehaviour
    {
        #region VARIABLES

        /// <summary>
        /// Game Camera Reference
        /// </summary>
        [Header("Game Camera Reference")]
        public CameraReference GameCamera;

        /// <summary>
        /// World Screen Height.
        /// </summary>
        [Header("World Screen Dimensions")]
        public FloatReference WorldScreenHeight;

        /// <summary>
        /// World Screen Width.
        /// </summary>
        public FloatReference WorldScreenWidth;

        /// <summary>
        /// Resolution State Reference.
        /// </summary>
        [Header("Resolution State")]
        public ResolutionState ResolutionState;

        /// <summary>
        /// Use New World Size Flag.
        /// </summary>
        [Header("Options")]
        [Tooltip("Use the new World Size Calculation Method")]
        public bool UseNewWorldSizeCalculation = false;

        /// <summary>
        /// Show Debug Information Flag.
        /// </summary>
        [Header("Debug")]
        public bool ShowDebugLog = false;

        /// <summary>
        /// Cached Screen Height.
        /// </summary>
        private float _screenHeight;

        /// <summary>
        /// Cached Screen Width.
        /// </summary>
        private float _screenWidth;

        /// <summary>
        /// Previous Screen Size.
        /// </summary>
        private Vector2 _previousScreenSize = Vector2.zero;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            if (!GameCamera.Camera)
            {
                GameCamera.Camera = Camera.main;
            }
        }

        private void LateUpdate()
        {
            if (!UseNewWorldSizeCalculation)
            {
                CalculateWorldSize(GameCamera.OrthographicSize, GameCamera.Aspect);
            }
            else
            {
                NewCalculateWorldSize(GameCamera.PixelRect);
            }
        }

        #endregion

        #region SCREEN SIZE MONITOR METHODS

        /// <summary>
        /// Calculate the World Size Simple.
        /// </summary>
        public void CalculateWorldSize(float orthographicSize, float aspect)
        {
            _screenHeight = orthographicSize * 2f;
            _screenWidth = _screenHeight     * aspect;

            if (_screenHeight != WorldScreenHeight && _screenWidth != WorldScreenWidth)
            {
                WorldScreenHeight.Value = _screenHeight / 2f;
                WorldScreenWidth.Value = _screenWidth   / 2f;

                if (ShowDebugLog)
                {
                    print($"Recalculating World Size.");
                }
            }
        }

        /// <summary>
        /// Calculate new World Size
        /// </summary>
        /// <param name="cameraPixelRect"></param>
        public void NewCalculateWorldSize(Rect cameraPixelRect)
        {
            int screenWidth = (int) cameraPixelRect.width;
            int screenHeight = (int) cameraPixelRect.height;

            if (_previousScreenSize.y != screenHeight && _previousScreenSize.x != screenWidth)
            {
                _previousScreenSize.y = screenHeight;
                _previousScreenSize.x = screenWidth;
                ResolutionState.CurrentScreenResolution = _previousScreenSize;

                if (ShowDebugLog)
                {
                    print($"Recalculating World Size.");
                }
            }
        }

        #endregion
    }
}