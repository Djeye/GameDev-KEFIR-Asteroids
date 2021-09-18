using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace asteroids.Core
{
    public static class Extension
    {
        static void Blabla()
        {

        }

        public static Vector3 GenerateSpawnPosition()
        {
            float spawnDistance = Camera.main.orthographicSize * 1.2f;
            float xPosition = spawnDistance, yPosition = spawnDistance;

            if (Random.Range(0, 2).Equals(0))
            {
                xPosition = Random.Range(-spawnDistance, spawnDistance);
            }
            else
            {
                yPosition = Random.Range(-spawnDistance, spawnDistance);
            }

            float randomXSign = Random.Range(0, 2) * 2 - 1;
            float randomYSign = Random.Range(0, 2) * 2 - 1;

            return new Vector3(randomXSign * xPosition, randomYSign * yPosition);
        }
    }
}
