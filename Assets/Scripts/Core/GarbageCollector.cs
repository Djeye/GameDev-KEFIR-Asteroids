using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace asteroids.Core
{
    public class GarbageCollector : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D collision)
        {
            Destroy(collision.gameObject);
        }
    }
}