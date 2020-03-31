// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using SOFlow.ScriptableObjects;
using UnityEngine;

namespace JellyFish.CameraUtilities
{
    [CreateAssetMenu(menuName = "SOFlow/Utilities/Camera Reference")]
    public class CameraReference : DropdownScriptableObject
    {
        /// <summary>
        ///     The camera reference.
        /// </summary>
        public Camera Camera;

        /// <summary>
        ///     Sets the camera reference.
        /// </summary>
        /// <param name="camera"></param>
        public void SetCameraReference(Camera camera)
        {
            Camera = camera;
        }
    }
}