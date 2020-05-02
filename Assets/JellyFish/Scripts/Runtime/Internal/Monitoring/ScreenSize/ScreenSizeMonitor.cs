/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */
using SOFlow.Data.Primitives;
using JellyFish.Internal.Utilities;
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
        /// Resolution State.
        /// </summary>
        [Header("Resolution State")]
        public ResolutionState ResolutionState;

        /// <summary>
        /// World Screen Height.
        /// </summary>
        [Header("World Screen Dimensions")]
        public FloatField WorldScreenHeight;

        /// <summary>
        /// World Screen Width.
        /// </summary>
        public FloatField WorldScreenWidth;

        /// <summary>
        /// Only Calculate World Size Flag.
        /// </summary>
        [Header("Settings")]
        public bool OnlyCalculateWorldSize = false;

        /// <summary>
        /// Only Calculate Screen Size Flag.
        /// </summary>
        public bool OnlyCalculateScreenSize = false;

        /// <summary>
        /// Previous Screen Size.
        /// </summary>
        private Vector2Int _previousScreenSize;

        /// <summary>
        /// Previous World Screen Size.
        /// </summary>
        private Vector2 _previousWorldScreenSize;

        #endregion

        #region UNITY METHODS

        private void LateUpdate()
        {
            CalculateScreenSizeAndWorldBounds(GameCamera.Camera.pixelRect, GameCamera.Camera.orthographicSize,
                                              GameCamera.Camera.aspect);
        }

        #endregion

        #region SCREEN SIZE MONITOR METHODS

        /// <summary>
        /// Calculate the World Size Simple.
        /// </summary>
        public void CalculateScreenSizeAndWorldBounds(Rect cameraPixelRect, float orthographicSize, float aspect)
        {
            if (OnlyCalculateWorldSize)
            {
                CalculateWorldSize(orthographicSize, aspect);
            }

            if (OnlyCalculateScreenSize)
            {
                CalculateScreenSize(cameraPixelRect);
            }
        }

        /// <summary>
        /// Calculate the World Size.
        /// </summary>
        /// <param name="orthographicSize"></param>
        /// <param name="aspect"></param>
        private void CalculateWorldSize(float orthographicSize, float aspect)
        {
            float worldHeight = orthographicSize;
            float worldWidth  = worldHeight * aspect;

            if (worldHeight != _previousWorldScreenSize.y || worldWidth != _previousWorldScreenSize.x)
            {
                WorldScreenHeight.Value = worldHeight;
                WorldScreenWidth.Value  = worldWidth;

                _previousWorldScreenSize.y = WorldScreenHeight;
                _previousWorldScreenSize.x = WorldScreenWidth;

                ResolutionState.CurrentWorldScreenSize = _previousWorldScreenSize;

                print("World Size Changed!");
            }
        }

        /// <summary>
        /// Calculate the Screen Resolution.
        /// </summary>
        /// <param name="cameraPixelRect"></param>
        private void CalculateScreenSize(Rect cameraPixelRect)
        {
            int screenHeight = (int) cameraPixelRect.height;
            int screenWidth  = (int) cameraPixelRect.width;

            if (screenWidth != _previousScreenSize.x && screenHeight != _previousScreenSize.y)
            {
                _previousScreenSize.x = screenWidth;
                _previousScreenSize.y = screenHeight;

                ResolutionState.CurrentScreenResolution = _previousScreenSize;

                print("Screen Resolution Changed!");
            }
        }

        /// <summary>
        /// Bound a Transform within the World.
        /// </summary>
        /// <param name="localPosition"></param>
        /// <returns></returns>
        public void BoundWithinScreen(ref Vector3 localPosition)
        {
            float worldHeight = WorldScreenHeight;
            float worldWidth  = WorldScreenWidth;

            localPosition.y = Mathf.Clamp(localPosition.y, -worldHeight, worldHeight);
            localPosition.x = Mathf.Clamp(localPosition.x, -worldWidth, worldWidth);
        }

        #endregion
    }
}