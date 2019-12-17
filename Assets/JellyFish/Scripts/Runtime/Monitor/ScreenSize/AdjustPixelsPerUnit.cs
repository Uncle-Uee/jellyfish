/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System.Collections.Generic;
using JellyFish.Data.Primitive;
using UltEvents;
using UnityEngine;

namespace JellyFish.Monitor.ScreenSize
{
    /// <summary>
    /// Orientations
    /// </summary>
    public enum Orientation
    {
        FaceDown           = 0,
        FaceUp             = 1,
        LandscapeLeft      = 2,
        LandscapeRight     = 3,
        Portrait           = 4,
        PortraitUpsideDown = 5,
        Unknown            = 6
    }

    public class AdjustPixelsPerUnit : MonoBehaviour
    {
        #region VARIABLES

        /// <summary>
        /// Pixel Perfect Camera Reference.
        /// </summary>
        [Header("Required Components")]
        public UnityEngine.U2D.PixelPerfectCamera PixelPerfectCamera;

        /// <summary>
        /// Master Pixels Per Unit Settings
        /// </summary>
        [Header("Master Pixels Per Unit")]
        public IntReference MasterPPU;

        /// <summary>
        /// List of different Pixels per unit.
        /// </summary>
        public List<IntReference> PixelsPerUnit = new List<IntReference>();

        /// <summary>
        /// Resolution State.
        /// </summary>
        [Header("Resolution State Reference")]
        public UltEvent ResolutionState;

        [Header("Options")]
        public bool AndroidBuild = false;

        /// <summary>
        /// Horizontal Pixels Per Unit.
        /// </summary>
        [Header("Layout PPU")]
        public FloatReference HorizontalPPU;

        /// <summary>
        /// Vertical Pixels Per Unit.
        /// </summary>
        public FloatReference VerticalPPU;


        /// <summary>
        /// Current Device Orientation
        /// </summary>
        [Header("Orientation Options")]
        public DeviceOrientation CurrentDeviceOrientation = DeviceOrientation.Unknown;


        /// <summary>
        /// Device Orientations.
        /// </summary>
        private static readonly DeviceOrientation[] _deviceOrientation =
        {
            DeviceOrientation.FaceDown,
            DeviceOrientation.FaceUp,
            DeviceOrientation.LandscapeLeft,
            DeviceOrientation.LandscapeRight,
            DeviceOrientation.Portrait,
            DeviceOrientation.PortraitUpsideDown,
            DeviceOrientation.Unknown,
        };

        #endregion


        #region UNITY METHODS

        private void Start()
        {
            CurrentDeviceOrientation = Input.deviceOrientation;
        }

        #endregion


        #region METHODS

        /// <summary>
        /// Adjust the PPU per Device Orientation.
        /// </summary>
        /// <param name="orientation"></param>
        public void AdjustPPUForDeviceOrientation(Orientation orientation)
        {
            if (Input.deviceOrientation == _deviceOrientation[(int) orientation])
            {
                CurrentDeviceOrientation = Input.deviceOrientation;
                PixelPerfectCamera.assetsPPU = MasterPPU;
            }
        }

        #endregion
    }
}