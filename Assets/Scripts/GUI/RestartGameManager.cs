using UnityEngine;
using asteroids.Core;

namespace asteroids.GUI
{
    public class RestartGameManager : MonoBehaviour
    {
        public void RestartActions() => Extension.Restart();
    }
}
