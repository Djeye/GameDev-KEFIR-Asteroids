using UnityEngine;
using UnityEngine.SceneManagement;

namespace asteroids.GUI
{
    public class SceneLoader : MonoBehaviour
    {
        public void ChangeScene(int sceneID) => SceneManager.LoadScene(sceneID);
    }
}
