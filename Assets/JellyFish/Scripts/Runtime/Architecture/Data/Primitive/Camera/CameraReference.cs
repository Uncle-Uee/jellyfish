/**
 * Created By: Ubaidullah Effendi-Emjedi
 * LinkedIn : https://www.linkedin.com/in/ubaidullah-effendi-emjedi-202494183/
 */

using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace JellyFish.Data.Primitive
{
    [Serializable]
    public class CameraReference
    {
        /// <summary>
        /// Constant Camera Value.
        /// </summary>
        public Camera ConstantValue;

        /// <summary>
        /// Flag to use either Constant or Variable
        /// </summary>
        public bool UseConstant = true;

        /// <summary>
        /// Camera Variable
        /// </summary>
        public CameraVariable Variable;

        #region PROPERTIES

        /// <summary>
        /// Camera Reference and Variable Value.
        /// </summary>
        public Camera Camera
        {
            get => UseConstant ? ConstantValue : Variable.Value;
            set
            {
                if (UseConstant)
                {
                    ConstantValue = value;
                }
                else
                {
                    Variable.Value = value;
                }
            }
        }

        /// <summary>
        /// Aspect Ratio
        /// </summary>
        public float Aspect => Camera.aspect;

        /// <summary>
        /// Culling Mask
        /// </summary>
        public float CullingMask => Camera.cullingMask;

        /// <summary>
        /// Depth
        /// </summary>
        public float Depth => Camera.depth;

        /// <summary>
        /// Event Mask
        /// </summary>
        public float EventMask => Camera.eventMask;

        /// <summary>
        /// Pixel Height
        /// </summary>
        public float PixelHeight => Camera.pixelHeight;

        /// <summary>
        /// Pixel Rect.
        /// </summary>
        public Rect PixelRect => Camera.pixelRect;

        /// <summary>
        /// Pixel Width
        /// </summary>
        public float PixelWidth => Camera.pixelWidth;

        /// <summary>
        /// Orthographic Size.
        /// </summary>
        public float OrthographicSize => Camera.orthographicSize;

        #endregion


        #region IMPLICIT OPERATOR METHOD

        public static implicit operator Camera(CameraReference reference)
        {
            return reference.Camera;
        }

        #endregion


        #region CONSTRUCTORS

        public CameraReference()
        {
        }

        public CameraReference(Camera value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        #endregion
    }
}