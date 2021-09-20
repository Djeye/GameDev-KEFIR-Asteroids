using UnityEngine;
using asteroids.Core;

namespace asteroids.MarsianShip
{
    public class MarsianManager : MonoBehaviour
    {
        [SerializeField] private Marsian marsian;
        // link to playership to follow it
        [SerializeField] private Transform player;
        [SerializeField] float marsianSpawnFrequency;

        private float _spawnTime, _spawnDelay;
        private void Start()
        {
            // prepare fow spawning 
            _spawnDelay = 1 / marsianSpawnFrequency;
            _spawnTime = _spawnDelay;
        }

        private void FixedUpdate()
        {
            SpawnMarsian();
        }

        private void SpawnMarsian()
        {
            if (_spawnTime < Time.time && !Extension.isGameEnded)
            {
                Instantiate(marsian, Extension.GenerateSpawnPosition(), Quaternion.identity, transform);
                _spawnTime = Time.time + _spawnDelay;
            }
        }

        public Transform GetPlayerTransform() { return player; }
    }
}
