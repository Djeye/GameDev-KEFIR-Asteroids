using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace asteroids.SpaceShip
{
    public class CannonManager : MonoBehaviour
    {
        [SerializeField] GameObject projectile;
        [SerializeField] GameObject laser;
        [SerializeField] float shootSpeedProjectile;
        [SerializeField] float laserDuration;

        private float _shootTime, _delayTime;
        private bool _isLaserActive = false;
        private PlayerMovement _player;

        // Start is called before the first frame update
        void Start()
        {
            _delayTime = 1f / shootSpeedProjectile;
            _shootTime = _delayTime;

            _player = GetComponent<PlayerMovement>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            ShootProjectile();
            ShootLaser();
        }

        private void ShootProjectile()
        {
            if (_shootTime < Time.time && _player.IsShipShootingCommon)
            {
                Instantiate(projectile, _player.ShipTransform.position, _player.ShipRotation);
                _shootTime = Time.time + _delayTime;
            }
        }

        private void ShootLaser()
        {
            if (_player.IsShipShootingSpecial && !_isLaserActive)
            {
                StartCoroutine(ShootLaserCoroutine());
            }
        }

        private IEnumerator ShootLaserCoroutine()
        {
            _isLaserActive = true;
            GameObject activelaser = Instantiate(laser, _player.ShipTransform);
            yield return new WaitForSecondsRealtime(laserDuration);
            Destroy(activelaser.gameObject);
            _isLaserActive = false;
        }

    }
}
