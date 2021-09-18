using asteroids.SpaceShip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace asteroids.Asteroids
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private int onDestroyPieces;
        [SerializeField] bool isDestroyable;
        [SerializeField] float velocity;

        private Vector3 flyDirection;
        private void Start()
        {
            flyDirection = ChooseRandomFlyDirection();
        }

        private void FixedUpdate()
        {
            transform.position += flyDirection * Time.fixedDeltaTime * velocity;
        }

        private Vector3 ChooseRandomFlyDirection()
        {
            Vector3 randomDirection = Vector3.zero;
            float cameraViewHalfSize = Camera.main.orthographicSize;

            randomDirection = new Vector3(Random.Range(-cameraViewHalfSize, cameraViewHalfSize),
                 Random.Range(-cameraViewHalfSize, cameraViewHalfSize));

            if (isDestroyable) randomDirection -= transform.position;

            return randomDirection.normalized;
        }

        public void SpawnAsteroidPieces()
        {
            if (isDestroyable)
            {
                for(int i=0; i < onDestroyPieces; i++)
                {
                    Instantiate(GetComponentInParent<AsteroidsManager>().GetAsteroidPiece(), transform.position, Quaternion.identity, transform.parent);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Projectile projectile))
            {
                Destroy(this.gameObject);
                if (projectile.IsDestroyable())
                {
                    Destroy(projectile.gameObject);
                    SpawnAsteroidPieces();
                }

            }
        }
    }
}
