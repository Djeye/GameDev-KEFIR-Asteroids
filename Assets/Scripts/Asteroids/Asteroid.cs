using UnityEngine;
using asteroids.Core;

namespace asteroids.Asteroids
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private int onDestroyPieces;
        [SerializeField] private bool isDestroyable;
        [SerializeField] private float velocity;

        private Vector3 _flyDirection;
        private void Start() => _flyDirection = ChooseRandomFlyDirection();


        private void FixedUpdate()
        {
            MoveAsteroid();
        }

        private void MoveAsteroid()
        {
            // move asteroid in choosen direction with given speed
            if (!Extension.isGameEnded) transform.position += _flyDirection * Time.fixedDeltaTime * velocity;
        }

        private Vector3 ChooseRandomFlyDirection()
        {
            Vector3 randomDirection = Vector3.zero;
            float cameraViewHalfSize = Camera.main.orthographicSize;

            // point in camera view, so asteroid on spawn should fly in player playfield
            randomDirection = new Vector3(Random.Range(-cameraViewHalfSize, cameraViewHalfSize),
                 Random.Range(-cameraViewHalfSize, cameraViewHalfSize));

            // if piece of asteroid it can fly in any direction
            if (isDestroyable) randomDirection -= transform.position;

            return randomDirection.normalized;
        }

        // spawn asteroid pieces when main asteroid was destroyed
        public void SpawnAsteroidPieces()
        {
            if (isDestroyable)
            {
                for(int i=0; i < onDestroyPieces; i++)
                {
                    Instantiate(GetComponentInParent<AsteroidsManager>().GetAsteroidPiece(), 
                        transform.position, Quaternion.identity, transform.parent);
                }
            }
        }
    }
}
