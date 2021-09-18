using asteroids.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace asteroids.Asteroids
{
    public class AsteroidsManager : MonoBehaviour
    {
        [SerializeField] List<Asteroid> asteroidsList;
        [SerializeField] Asteroid asteroidPiece;

        [SerializeField] float asteroidsFallFrequency;

        private float _fallTime, _fallDelay;
        // Start is called before the first frame update
        void Start()
        {
            _fallDelay = 1 / asteroidsFallFrequency;
            _fallTime = _fallDelay;
        }

        // Update is called once per frame
        void Update()
        {
            SpawnAsteroid();
        }

        private void SpawnAsteroid()
        {
            if (_fallTime < Time.time)
            {
                Instantiate(asteroidsList[Random.Range(0, asteroidsList.Count - 1)], Extension.GenerateSpawnPosition(), Quaternion.identity, transform);
                _fallTime = Time.time + _fallDelay;
            }
        }

        public Asteroid GetAsteroidPiece() { return asteroidPiece; }
    }
}
