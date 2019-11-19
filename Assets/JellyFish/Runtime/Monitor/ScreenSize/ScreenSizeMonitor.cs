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

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            if (!GameCamera.Value)
            {
                GameCamera.Value = Camera.main;
            }
        }

        private void LateUpdate()
        {
            CalculateWorldSize(GameCamera.Value.orthographicSize, GameCamera.Value.aspect);
        }

        #endregion


        #region SCREEN SIZE MONITOR METHODS

        /// <summary>
        /// Calculate the World Size Simple.
        /// </summary>
        public void CalculateWorldSize(float orthographicSize, float aspect)
        {
            _screenHeight = orthographicSize * 2f;
            _screenWidth  = _screenHeight    * aspect;

            if (WorldScreenHeight != _screenHeight && _screenWidth != WorldScreenWidth)
            {
                WorldScreenHeight.Value = _screenHeight;
                WorldScreenWidth.Value  = _screenWidth;

                if (ShowDebugLog)
                {
                    print($"Recalculating World Size.");
                }
            }
        }

        #endregion
    }
}