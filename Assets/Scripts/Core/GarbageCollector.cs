using UnityEngine;

namespace asteroids.Core
{
    public class GarbageCollector : MonoBehaviour
    {
        // destroy all objects which exit collector collider for saving memory
        private void OnTriggerExit2D(Collider2D collision) => Destroy(collision.gameObject);

    }
}