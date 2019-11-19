using UnityEngine;

namespace Ship.Movement
{
    public class Movement : MonoBehaviour
    {
        #region VARIABLES

        /// <summary>
        /// Movement Speed.
        /// </summary>
        [Header("Movement Fields")]
        public float MovementSpeed;

        /// <summary>
        /// Movement Speed Smoothing.
        /// </summary>
        public float MovementSpeedSmoothing;

        /// <summary>
        /// Horizontal Axis.
        /// </summary>
        [Header("Input Axis")]
        public float HorizontalAxis;

        /// <summary>
        /// Vertical Axis.
        /// </summary>
        public float VerticalAxis;

        /// <summary>
        /// Target Position.
        /// </summary>
        private Vector3 _targetPosision;

        #endregion


        #region UNITY METHODS

        private void Update()
        {
            HorizontalAxis = Input.GetAxisRaw("Horizontal");
            VerticalAxis   = Input.GetAxisRaw("Vertical");

            _targetPosision.x = HorizontalAxis;
            _targetPosision.y = VerticalAxis;

            transform.position += Time.deltaTime * MovementSpeed * _targetPosision;
        }

        #endregion
    }
}