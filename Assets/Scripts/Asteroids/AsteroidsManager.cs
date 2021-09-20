using asteroids.Core;
using System.Collections.Generic;
using UnityEngine;

namespace asteroids.Asteroids
{
    public class AsteroidsManager : MonoBehaviour
    {
        // serialize several main asteroids with different parameters
        [SerializeField] private List<Asteroid> asteroidsList;
        [SerializeField] private Asteroid asteroidPiece;

        [SerializeField] private float asteroidsFallFrequency;

        private float _fallTime, _fallDelay;

        private void Start()
        {
            // prepare for continous spawn
            _fallDelay = 1 / asteroidsFallFrequency;
            _fallTime = _fallDelay;
        }

        private void FixedUpdate()
        {
            SpawnAsteroid();
        }

        private void SpawnAsteroid()
        {
            if (_fallTime < Time.time && !Extension.isGameEnded)
            {
                Instantiate(asteroidsList[Random.Range(0, asteroidsList.Count - 1)], 
                    Extension.GenerateSpawnPosition(), Quaternion.identity, transform);
                _fallTime = Time.time + _fallDelay;
            }
        }

        // because asteroids itself cant attach with piece
        // they get it by link from here in parent
        public Asteroid GetAsteroidPiece() { return asteroidPiece; }
    }
}
