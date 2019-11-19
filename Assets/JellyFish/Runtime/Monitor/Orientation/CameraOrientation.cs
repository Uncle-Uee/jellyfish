/**
 * Created By: Ubaidullah Effendi-Emjedi
 * Alias: Uee
 *
 * Modified By:
 *
 * Created On: 15 November 2019
 */

using System.Collections;
using System.Collections.Generic;
using JellyFish.Data.Primitive;
using JellyFish.Events;
using UnityEngine;
using UnityEngine.U2D;

// ReSharper disable once CheckNamespace
namespace JellyFish.Monitor.Orientation
{
    public class CameraOrientation : MonoBehaviour
    {
        #region VARIABLESv

        /// <summary>
        /// Pixel Perfect Camera Reference.
        /// </summary>
        [Header("Pixel Perfect Camera")]
        public PixelPerfectCamera PixelPerfectCamera;


        /// <summary>
        /// List of Preferred Device Orientations.
        /// </summary>
        [Header("Preferred Orientations")]
        public List<DeviceOrientation> PreferredOrientations = new List<DeviceOrientation>();

        /// <summary>
        /// List of GameEvents.
        /// </summary>
        [Header("On Orientation Changed GameEvents")]
        public List<GameEvent> OnOrientationChangedEvents = new List<GameEvent>();

        #endregion


        #region UNITY METHODS

        private IEnumerator Start()
        {
            DeviceOrientation orientation        = Input.deviceOrientation;
            DeviceOrientation currentOrientation = orientation;

            ChangePixelPerUnits(ref orientation, ref currentOrientation);

            while (Application.isPlaying)
            {
                yield return new WaitUntil(() =>
                {
                    currentOrientation = Input.deviceOrientation;

                    if (!PreferredOrientations.Contains(currentOrientation))
                    {
                        return false;
                    }

                    return true;
                });

                ChangePixelPerUnits(ref orientation, ref currentOrientation);
            }
        }

        #endregion


        #region CAMERA ORIENTATION METHODS

        /// <summary>
        /// Change Assets Pixel Per Unit.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void ChangeAssetPixelsPerUnit(IntVariable value)
        {
            PixelPerfectCamera.assetsPPU = value.Value;
        }


        /// <summary>
        /// Change The Pixel Per Units On Screen Orientation Change.
        /// </summary>
        /// <param name="orientation"></param>
        /// <param name="currentOrientation"></param>
        private void ChangePixelPerUnits(ref DeviceOrientation orientation, ref DeviceOrientation currentOrientation)
        {
            print("Changing PPU.");


            for (int i = 0; i < PreferredOrientations.Count; i++)
            {
                if (currentOrientation == PreferredOrientations[i])
                {
                    // ToDo: Change Orientation.
                    orientation = currentOrientation;
                    // ToDo: Raise Event.
                    OnOrientationChangedEvents[i]?.Raise();
                }
            }
        }

        #endregion
    }
}