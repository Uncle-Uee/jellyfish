// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using UnityEngine;

namespace JellyFish.PlayerInput
{
    public class LookAtMousePosition : MonoBehaviour
    {
        /// <summary>
        ///     The look at plane.
        /// </summary>
        private Plane _lookAtPlane;

        /// <summary>
        ///     Indicates whether the x axis should be ignored.
        /// </summary>
        public bool IgnoreX;

        /// <summary>
        ///     Indicates whether the y axis should be ignored.
        /// </summary>
        public bool IgnoreY;

        /// <summary>
        ///     Indicates whether the z axis should be ignored.
        /// </summary>
        public bool IgnoreZ;

        private void Awake()
        {
            _lookAtPlane = new Plane(Vector3.up, transform.position);
        }

        private void Update()
        {
            UpdateLookAtDirection();
        }

        /// <summary>
        ///     Updates the look at direction of this transform to match the current position of the mouse.
        /// </summary>
        public void UpdateLookAtDirection()
        {
            if(Camera.main != null)
            {
                _lookAtPlane.SetNormalAndPosition(Vector3.up, transform.position);

                Ray   lookAtRay      = Camera.main.ScreenPointToRay(Input.mousePosition);
                float lookAtDistance = 0f;

                if(_lookAtPlane.Raycast(lookAtRay, out lookAtDistance))
                {
                    Vector3 lookAtPosition = lookAtRay.GetPoint(lookAtDistance);

                    lookAtPosition.x = IgnoreX ? transform.position.x : lookAtPosition.x;
                    lookAtPosition.y = IgnoreY ? transform.position.y : lookAtPosition.y;
                    lookAtPosition.z = IgnoreZ ? transform.position.z : lookAtPosition.z;

                    transform.LookAt(lookAtPosition);
                }
            }
        }
    }
}