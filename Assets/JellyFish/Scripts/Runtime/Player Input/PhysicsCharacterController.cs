// Created by Kearan Petersen : https://www.blumalice.wordpress.com | https://www.linkedin.com/in/kearan-petersen/

using SOFlow.Data.Events;
using UltEvents;
using UnityEngine;

namespace JellyFish.PlayerInput
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsCharacterController : MonoBehaviour
    {
        /// <summary>
        ///     The rigidbody reference.
        /// </summary>
        public Rigidbody Rigidbody;
        
        /// <summary>
        ///     The horizontal axis.
        /// </summary>
        public string HorizontalAxis = "Horizontal";

        /// <summary>
        ///     The vertical axis.
        /// </summary>
        public string VerticalAxis = "Vertical";

        /// <summary>
        ///     The max speed.
        /// </summary>
        public float MaxSpeed = 2f;

        /// <summary>
        ///     The move speed.
        /// </summary>
        public float MoveSpeed = 3000f;

        /// <summary>
        ///     The rotation speed.
        /// </summary>
        public float RotationSpeed = 3f;

        /// <summary>
        ///     Indicates whether this component should move the target rigidbody.
        /// </summary>
        public bool MoveCharacter = true;

        /// <summary>
        ///     Indicates whether this component should rotate the target rigidbody.
        /// </summary>
        public bool RotateCharacter = true;

        /// <summary>
        ///     Event raised when the character starts moving.
        /// </summary>
        [Header("Events")]
        public UltEvent OnCharacterStartMoving;

        /// <summary>
        ///     Event raised when the character starts rotating.
        /// </summary>
        public UltEvent OnCharacterStartRotating;

        /// <summary>
        ///     Event raised when the character stops moving.
        /// </summary>
        public UltEvent onCharacterStopMoving;

        /// <summary>
        ///     Event raised when the character stops rotating.
        /// </summary>
        public UltEvent OnCharacterStopRotating;

        /// <summary>
        ///     The clamped velocity.
        /// </summary>
        private Vector3 _clampedVelocity;

        /// <summary>
        ///     The fall speed.
        /// </summary>
        private float _fallSpeed;

        /// <summary>
        ///     The target rotation.
        /// </summary>
        private float _targetRotation;

        /// <summary>
        ///     The x axis input.
        /// </summary>
        private float _xAxisInput;

        /// <summary>
        ///     The z axis input.
        /// </summary>
        private float _zAxisInput;

        /// <summary>
        ///     Indicates whether this character is currently moving.
        /// </summary>
        public bool CharacterMoving
        {
            get;
            private set;
        }

        /// <summary>
        ///     Indicates whether this character is currently rotating.
        /// </summary>
        public bool CharacterRotating
        {
            get;
            private set;
        }

        private void Awake()
        {
            if(!Rigidbody)
            {
                Rigidbody                = GetComponent<Rigidbody>();
                Rigidbody.freezeRotation = true;
            }
        }

        private void Update()
        {
            _xAxisInput = Input.GetAxis(HorizontalAxis);
            _zAxisInput = Input.GetAxis(VerticalAxis);

            if(MoveCharacter) UpdateCharacterMovement();

            if(RotateCharacter) UpdateCharacterRotation();
        }

        /// <summary>
        ///     Moves the character according to the input axis.
        /// </summary>
        public void UpdateCharacterMovement()
        {
            if(Rigidbody)
            {
                if(_xAxisInput != 0f || _zAxisInput != 0f)
                {
                    if(!CharacterMoving)
                    {
                        CharacterMoving = true;
                        OnCharacterStartMoving.Invoke();
                    }
                }
                else
                {
                    if(CharacterMoving)
                    {
                        CharacterMoving = false;
                        onCharacterStopMoving.Invoke();
                    }
                }

                Rigidbody.AddForce(new Vector3(_xAxisInput, 0f, _zAxisInput) * MoveSpeed *
                                   Time.deltaTime);

                if(Rigidbody.velocity.magnitude > MaxSpeed)
                {
                    _fallSpeed         = Rigidbody.velocity.y;
                    _clampedVelocity   = Vector3.ClampMagnitude(Rigidbody.velocity, MaxSpeed);
                    _clampedVelocity.y = _fallSpeed;

                    Rigidbody.velocity = _clampedVelocity;
                }
            }
        }

        /// <summary>
        ///     Updates the character rotation according to the input axis.
        /// </summary>
        public void UpdateCharacterRotation()
        {
            _targetRotation = transform.localEulerAngles.y;

            if(_zAxisInput > 0f)
            {
                if(_xAxisInput > 0f)
                    _targetRotation = 45f;
                else if(_xAxisInput < 0f)
                    _targetRotation = 315f;
                else
                    _targetRotation = 0f;
            }
            else if(_zAxisInput < 0f)
            {
                if(_xAxisInput > 0f)
                    _targetRotation = 135f;
                else if(_xAxisInput < 0f)
                    _targetRotation = 225f;
                else
                    _targetRotation = 180f;
            }
            else
            {
                if(_xAxisInput > 0f)
                    _targetRotation                       = 90f;
                else if(_xAxisInput < 0f) _targetRotation = 270f;
            }

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                                                     Mathf.LerpAngle(transform.localEulerAngles.y, _targetRotation,
                                                                     Time.deltaTime * RotationSpeed),
                                                     transform.localEulerAngles.z);
        }
    }
}