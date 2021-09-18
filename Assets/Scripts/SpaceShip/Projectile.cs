using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace asteroids.SpaceShip
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] bool isFlying;

        // Update is called once per frame
        void Update()
        {
            if (isFlying)
                transform.position += transform.up * Time.deltaTime * speed;
        }

        public bool IsDestroyable() { return isFlying; }
    }
}
