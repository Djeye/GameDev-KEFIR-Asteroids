using UnityEngine;
using asteroids.MarsianShip;
using asteroids.Asteroids;
using asteroids.Core;

namespace asteroids.SpaceShip
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed, acceleration;

        private Vector3 _prevFramePosition, _prevFrameVelocity;
        private PlayerInput _playerInput;

        private void Awake() => _playerInput = GetComponent<PlayerInput>();

        private void FixedUpdate()
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

            // only forward moving
            float _rawMovementTowards = Mathf.Clamp(_playerInput.GetMovementVector().y, 0, 1);

            // True Acceleration is acceleration when player press move button multiplied by directional vector of spaceship
            Vector3 trueAcceleration = acceleration * _rawMovementTowards * transform.up;

            // Change position by calculating inertia from last frame, plus acceleration in new frame
            transform.position += _prevFrameVelocity * Time.deltaTime + trueAcceleration * Mathf.Pow(Time.deltaTime, 2) * 0.5f;

            // Calculate velocity by deviding difference between new and old positions at deltaTime
            _prevFrameVelocity = (transform.position - _prevFramePosition) / Time.deltaTime;
        }

        // Rotate spaceship by player move buttons
        private void ApplyRotation() => transform.Rotate(0, 0, -_playerInput.GetMovementVector().x * Time.deltaTime * rotateSpeed);

        #endregion

        #region Getters
        public Transform ShipTransform { get { return transform; } }

        public Quaternion ShipRotation { get { return transform.rotation; } }

        public float ShipVelocity { get { return _prevFrameVelocity.magnitude; } }
        #endregion


        // check for touching other dangerous objects
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // marsian or asteroid provide end of game
            if (collision.TryGetComponent(out Marsian marsian) || collision.TryGetComponent(out Asteroid asteroid))
            {
                Extension.SetEndOfGame();
                Destroy(this.gameObject);
            }
        }
    }
}