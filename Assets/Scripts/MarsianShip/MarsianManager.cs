using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using asteroids.Core;

namespace asteroids.MarsianShip
{
    public class MarsianManager : MonoBehaviour
    {
        [SerializeField] private Marsian marsian;
        [SerializeField] private Transform player;

        [SerializeField] float marsianSpawnFrequency;

        private float _spawnTime, _spawnDelay;
        // Start is called before the first frame update
        void Start()
        {
            _spawnDelay = 1 / marsianSpawnFrequency;
            _spawnTime = _spawnDelay;
        }

        // Update is called once per frame
        void Update()
        {
            SpawnMarsian();
        }

        private void SpawnMarsian()
        {
            if (_spawnTime < Time.time)
            {
                Instantiate(marsian, Extension.GenerateSpawnPosition(), Quaternion.identity, transform);
                _spawnTime = Time.time + _spawnDelay;
            }
        }

        public Transform GetPlayerTransform() { return player; }
    }
}
