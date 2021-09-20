using UnityEngine;
using asteroids.MarsianShip;
using asteroids.Asteroids;
using asteroids.Core;

namespace asteroids.SpaceShip
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private bool isProjectile;

        // transform of player to attach laser with it
        private Transform _playerTransform;

        private void FixedUpdate()
        {
            ApplyShooting();
        }

        private void ApplyShooting()
        {
            if (!Extension.isGameEnded)
            {
                if (isProjectile)
                    // if bullet projectile it moves with speed
                    transform.position += transform.up * Time.deltaTime * speed;
                else
                {
                    // if laser it moves with playership
                    transform.position = _playerTransform.position;
                    transform.rotation = _playerTransform.rotation;
                }
            }
        }

        // link player transform with laser
        public void LinkShipTransform(Transform playerTransform) => _playerTransform = playerTransform;

        // check entries with other objects
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Marsian marsian))
            {
                // add score
                Extension.score++;
                Destroy(marsian.gameObject);
                // if bullet projectile touch other object it destroy itself
                if (isProjectile)
                    Destroy(this.gameObject);
            }
            else if (collision.TryGetComponent(out Asteroid asteroid))
            {
                // add score
                Extension.score++;
                Destroy(asteroid.gameObject);
                // if bullet projectile touch other object it destroy itself
                if (isProjectile)
                {
                    Destroy(this.gameObject);
                    // alse it forces main asteroids to soawn his pieces 
                    asteroid.SpawnAsteroidPieces();
                }
            }
        }
    }
}
