using UnityEngine;
using asteroids.Core;

namespace asteroids.MarsianShip
{
    public class Marsian : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        private Transform _playerShip;

        private void Start()
        {
            // get link of player ship to follow it
            _playerShip = transform.parent.GetComponent<MarsianManager>().GetPlayerTransform();
        }

        void FixedUpdate()
        {
            MoveMarsianToPlayer();
        }

        private void MoveMarsianToPlayer()
        {
            if (!Extension.isGameEnded)
            {
                Vector3 moveToVector = Vector3.zero;

                // if player ship exists
                if (!_playerShip.Equals(null))
                {
                    // create normalized direction vector
                    moveToVector = (_playerShip.position - transform.position).normalized;
                }

                // change position with given speed
                transform.position += moveToVector * Time.fixedDeltaTime * moveSpeed;
            }
        }
    }
}
