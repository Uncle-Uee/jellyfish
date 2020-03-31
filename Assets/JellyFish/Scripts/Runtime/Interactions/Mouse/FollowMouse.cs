using JellyFish.CameraUtilities;
using SOFlow.Data.Primitives;
using UnityEngine;

namespace JellyFish.Interactions.Mouse
{
    public class FollowMouse : MonoBehaviour
    {
        #region VARIABLES

        /// <summary>
        /// Follow Mouse only if Follow flag is True
        /// </summary>
        [Header("Settings")]
        public BoolField Follow;

        /// <summary>
        /// Use Follow Mouse in 2D Space.
        /// </summary>
        public bool UseIn2DSpace = true;

        /// <summary>
        /// Use Spherical Linear Interpolation.
        /// </summary>
        [Header("Lerp Settings")]
        public BoolField UseSlerp;

        /// <summary>
        /// Current Camera
        /// </summary>
        [Header("Camera Reference")]
        public CameraReference CameraReference;

        /// <summary>
        /// Linear Interpolation Speed
        /// </summary>
        [Header("Object Movement")]
        public FloatField LerpSpeed;

        /// <summary>
        /// Current Objects Position.
        /// </summary>
        [Header("Orientation")]
        public Vector3Field CurrentPosition;

        /// <summary>
        /// Mouse Position
        /// </summary>
        private Vector3 _mousePosition;

        /// <summary>
        /// Cached Linear Interpolation Vector3
        /// </summary>
        private Vector3 _lerpVector = Vector3.zero;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Scene Camera.
        /// </summary>
        private Camera Camera => CameraReference.Camera;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            _mousePosition = Input.mousePosition;
        }


        private void Update()
        {
            ObjectFollowMouse();
            // print($"From Follow Mouse; Mouse Position - {_mousePosition}");
        }

        #endregion

        #region METHODS

        /// <summary>
        /// This Object Follow Mouse
        /// </summary>
        public void ObjectFollowMouse()
        {
            if (!Camera || !Follow) return;

            CurrentPosition.Value = transform.position;
            _mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);

            if (UseIn2DSpace)
            {
                _mousePosition.z = 0f;
            }

            if (!UseSlerp)
            {
                // transform.position = Vector3.Lerp(CurrentPosition.Value, _mousePosition, LerpSpeed);
                LerpObjectToMousePosition();
            }
            else
            {
                transform.position = Vector3.Slerp(CurrentPosition.Value, _mousePosition, LerpSpeed);
            }
        }

        /// <summary>
        /// Linear Interpolate a Vector2.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        private void LerpObjectToMousePosition()
        {
            LerpSpeed.Value = Mathf.Clamp01(LerpSpeed);

            _lerpVector.x = CurrentPosition.Value.x + (_mousePosition.x - CurrentPosition.Value.x) * LerpSpeed;
            _lerpVector.y = CurrentPosition.Value.y + (_mousePosition.y - CurrentPosition.Value.y) * LerpSpeed;

            if (!UseIn2DSpace)
            {
                _lerpVector.z = CurrentPosition.Value.z + (_mousePosition.z - CurrentPosition.Value.z) * LerpSpeed;
            }

            transform.position = _lerpVector;
        }

        #endregion
    }
}