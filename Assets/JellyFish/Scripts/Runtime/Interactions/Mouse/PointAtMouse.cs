using SOFlow.Data.Primitives;
using JellyFish.Internal.Utilities;
using UnityEngine;

namespace JellyFish.Interactions.Mouse
{
    public class PointAtMouse : MonoBehaviour
    {
        #region VARIABLES

        /// <summary>
        /// Point At Mouse only if Point flag is True
        /// </summary>
        [Header("Settings")]
        public BoolField Point;

        /// <summary>
        /// Use Point At Mouse in 2D Space.
        /// </summary>
        public bool UseIn2DSpace;

        /// <summary>
        /// Use Mouse Cursor Position.
        /// </summary>
        [Header("Pointer Settings")]
        public bool UseCursorPosition;

        /// <summary>
        /// Current Camera
        /// </summary>
        [Header("Camera Reference")]
        public CameraReference CameraReference;

        /// <summary>
        /// Current Mouse Cursor Position.
        /// </summary>
        [Header("Orientation")]
        public Vector3Field CursorPosition;

        /// <summary>
        /// Cached Mouse Position
        /// </summary>
        private Vector3 _mousePosition;

        /// <summary>
        /// Directional Vector; Difference between this Objects position and Mouse position.
        /// </summary>
        private Vector3 _delta;

        /// <summary>
        /// Calculated Z Rotation Value.
        /// </summary>
        private float _rotationZ;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Parent Transform
        /// </summary>
        private Transform Parent => gameObject.Parent();

        /// <summary>
        /// Scene Camera.
        /// </summary>
        private Camera Camera => CameraReference.Camera;

        #endregion

        #region UNITY METHODS

        private void Update()
        {
            PointTowardsMouse();
            // print($"From Point At Mouse; Mouse Position - {_mousePosition}");
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Point Object Towards the Mouse.
        /// </summary>
        private void PointTowardsMouse()
        {
            if (!Point) return;

            if (!UseCursorPosition)
            {
                _mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);

                if (UseIn2DSpace)
                {
                    _mousePosition.z = 0f;
                }
            }
            else
            {
                _mousePosition = CursorPosition.Value;
            }

            _delta = _mousePosition - transform.position;
            // _delta.Normalize();

            _rotationZ = Mathf.Atan2(_delta.y, _delta.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, _rotationZ);

            if (_rotationZ <= -90f || _rotationZ >= 90f)
            {
                // Rotate Sprite Based On Parent Orientation
                if ((int) Parent.eulerAngles.y == 0)
                {
                    transform.localRotation = Quaternion.Euler(180f, 0f, -_rotationZ);
                }
                // Rotate Sprite Based On Parent Orientation
                else if ((int) Parent.eulerAngles.y == 180 || (int) Parent.eulerAngles.y == 180)
                {
                    transform.localRotation = Quaternion.Euler(180f, 180f, -_rotationZ);
                }
            }
        }

        #endregion
    }
}