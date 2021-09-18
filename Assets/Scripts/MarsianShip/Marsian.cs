using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using asteroids.SpaceShip;

namespace asteroids.MarsianShip
{
    public class Marsian : MonoBehaviour
    {
        [SerializeField] float moveSpeed;
        private Transform _playerShip;

        void Start()
        {
            _playerShip = transform.parent.GetComponent<MarsianManager>().GetPlayerTransform();
        }

        void FixedUpdate()
        {
            MoveMarsianToPlayer();
        }

        private void MoveMarsianToPlayer()
        {
            Vector3 moveToVector = (_playerShip.position - transform.position).normalized;

            transform.position += moveToVector * Time.fixedDeltaTime * moveSpeed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Projectile projectile))
            {
                Destroy(this.gameObject);
                if (projectile.IsDestroyable())
                    Destroy(projectile.gameObject);
            }
        }
    }
}
