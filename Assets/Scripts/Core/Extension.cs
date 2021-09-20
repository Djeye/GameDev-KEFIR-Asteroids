using UnityEngine;

namespace asteroids.Core
{
    public static class Extension
    {
        public static bool isGameEnded = false;
        public static int score = 0;

        // event of end of game
        public delegate void EndOfGame();
        public static event EndOfGame endOfGameEvent;

        // triggered when player dies
        public static void SetEndOfGame()
        {
            isGameEnded = true;
            if (endOfGameEvent != null) endOfGameEvent.Invoke();
        }

        // change parameters on started ones when restarted
        public static void Restart()
        {
            isGameEnded = false;
            score = 0;
        }

        // return random point outside cameraview to hide spawn from player
        public static Vector3 GenerateSpawnPosition()
        {
            // get value of side outside camera view
            float spawnDistance = Camera.main.orthographicSize * 1.2f;
            float xPosition = spawnDistance, yPosition = spawnDistance;

            // randomly choose between horizontal and vertical sides
            if (Random.Range(0, 2).Equals(0))
            {
                xPosition = Random.Range(-spawnDistance, spawnDistance);
            }
            else
            {
                yPosition = Random.Range(-spawnDistance, spawnDistance);
            }

            // randomly choose one side between two horizontal or vertical sides
            // return random sign (- or +)
            float randomXSign = Random.Range(0, 2) * 2 - 1;
            float randomYSign = Random.Range(0, 2) * 2 - 1;

            return new Vector3(randomXSign * xPosition, randomYSign * yPosition);
        }
    }
}
