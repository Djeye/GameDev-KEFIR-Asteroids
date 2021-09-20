using System.Collections;
using UnityEngine;

namespace asteroids.SpaceShip
{
    public class CannonManager : MonoBehaviour
    {
        // attach projectiles objects from prefabs
        // enter projectiles parameters
        [SerializeField] private GameObject projectile;
        [SerializeField] private GameObject laser;
        [SerializeField] private float shootSpeedProjectile;
        [SerializeField] private float laserDuration;
        [SerializeField] private int laserCharges;
        [SerializeField] private float laserRechargeTime;

        private float _shootTime, _delayTime;
        private bool _isLaserActive = false;

        // current laser properties
        private int _currentLaserCharges;
        private float _currentRechargeTime;

        private PlayerMovement _player;
        private PlayerInput _playerInput;
        private GameObject _activelaser;

        private void Start()
        {
            // prepare for continuous shooting 
            _delayTime = 1f / shootSpeedProjectile;
            _shootTime = _delayTime;

            // initialize given charges parameters
            _currentLaserCharges = laserCharges;
            _currentRechargeTime = 0f;

            // get player components 
            _player = GetComponent<PlayerMovement>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void FixedUpdate()
        {
            ShootProjectile();
            ShootLaser();
        }

        private void ShootProjectile()
        {
            // shoot with given frequency
            if (_shootTime < Time.time && _playerInput.GetCommonAttackValue())
            {
                Instantiate(projectile, _player.ShipTransform.position, _player.ShipRotation);
                _shootTime = Time.time + _delayTime;
            }
        }

        private void ShootLaser()
        {
            // shoot only when we have charges and laser is inactive
            if (_playerInput.GetSpecialAttackValue() && !_isLaserActive && _currentLaserCharges > 0)
            {
                StartCoroutine(ShootLaserCoroutine());
            }
            // recharging every time unless charges are full
            Recharging();
        }

        private IEnumerator ShootLaserCoroutine()
        {
            // change charges parameters
            _currentLaserCharges--;
            _currentRechargeTime += laserRechargeTime;

            // dont let shoot while laser is active
            _isLaserActive = true;
            _activelaser = Instantiate(laser, _player.ShipTransform.position, _player.ShipRotation);
            _activelaser.GetComponent<Projectile>().LinkShipTransform(_player.ShipTransform);

            // wait laser duration then destroy laser
            yield return new WaitForSecondsRealtime(laserDuration);
            Destroy(_activelaser.gameObject);
            _isLaserActive = false;
        }

        private void Recharging()
        {
            // recharge only when miss charges
            if (!_currentLaserCharges.Equals(laserCharges))
            {
                // recharges only for full charges by waiting mutual time
                _currentRechargeTime = Mathf.Clamp(_currentRechargeTime - Time.fixedDeltaTime, 0, laserCharges * laserRechargeTime);
                if (_currentRechargeTime.Equals(0)) _currentLaserCharges = laserCharges;
            }
        }

        // there are situations when laser is active and player dies
        // so destroy laser with ship as well
        private void OnDestroy()
        {
            if (_activelaser) Destroy(_activelaser.gameObject);
        }

        #region Getters
        public float GetLaserRechargeTime { get { return _currentRechargeTime; } }
        public int GetLaserCharges { get { return _currentLaserCharges; } }
        #endregion
    }
}
