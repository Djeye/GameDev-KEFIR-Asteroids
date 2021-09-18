using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace asteroids.SpaceShip
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float rotateSpeed, acceleration;
        private Vector3 _rawMovementVector;
        private bool _rawAttackValue, _rawSpecialAttackValue;

        private Vector3 _prevFramePosition, _prevFrameVelocity;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            ApplyMovement();
            ApplyRotation();
            ApplySideTeleport();
        }

        #region ApplyMovements methods

        private void ApplySideTeleport()
        {
            // cameraEdge is the half size of edge of square camera view
            float cameraViewHalfSize = Camera.main.orthographicSize;

            // if player outside of camera view teleport him to opposite side
            if (Mathf.Abs(transform.position.x) > cameraViewHalfSize)
            {
                transform.position = new Vector3(-1 * cameraViewHalfSize * Mathf.Sign(transform.position.x), transform.position.y);
            }
            if (Mathf.Abs(transform.position.y) > cameraViewHalfSize)
            {
                transform.position = new Vector3(transform.position.x, -1 * cameraViewHalfSize * Mathf.Sign(transform.position.y));
            }
        }

        private void ApplyMovement()
        {
            // Calculate position in the last frame at start of new frame
            _prevFramePosition = transform.position;

            // True Acceleration is acceleration when player press move button multiplied by directional vector of spaceship
            Vector3 trueAcceleration = acceleration * _rawMovementVector.y * transform.up;

            // Change position by calculating inertia from last frame, plus acceleration in new frame
            transform.position += _prevFrameVelocity * Time.deltaTime + trueAcceleration * Mathf.Pow(Time.deltaTime, 2) * 0.5f;

            // Calculate velocity by deviding difference between new and old positions at deltaTime
            _prevFrameVelocity = (transform.position - _prevFramePosition) / Time.deltaTime;
        }

        // Rotate spaceship by player move buttons
        private void ApplyRotation() => transform.Rotate(0, 0, -_rawMovementVector.x * Time.deltaTime * rotateSpeed);

        #endregion

        #region Getters
        public Transform ShipTransform
        {
            get
            {
                return transform;
            }
        }

        public Quaternion ShipRotation
        {
            get
            {
                return transform.rotation;
            }
        }

        public bool IsShipShootingCommon
        {
            get
            {
                return _rawAttackValue;
            }
        }

        public bool IsShipShootingSpecial
        {
            get
            {
                return _rawSpecialAttackValue;
            }
        }

        #endregion

        #region Input System methods
        public void OnMovement(InputAction.CallbackContext value)
        {
            Vector2 inputValue = value.ReadValue<Vector2>();
            _rawMovementVector = (Vector3)inputValue;
        }

        public void OnCommonAttack(InputAction.CallbackContext value)
        {
            _rawAttackValue = value.performed;
        }

        public void OnSpecialAttack(InputAction.CallbackContext value)
        {
            _rawSpecialAttackValue = value.performed;
        }
        #endregion
    }
}