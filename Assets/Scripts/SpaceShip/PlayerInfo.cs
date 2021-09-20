using UnityEngine;
using TMPro;
using System;
using asteroids.Core;

namespace asteroids.SpaceShip
{
    public class PlayerInfo : MonoBehaviour
    {
        // attach gui objects to whow info
        [SerializeField] TextMeshProUGUI guiText;
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] GameObject endPanel;
        [SerializeField] TextMeshProUGUI endPanelScore;

        private PlayerMovement _playerMovement;
        private CannonManager _cannonManager;

        private Vector2 _coordinates;
        private float _rotationAngle, _instantSpeed, _laserRechargeTime;
        private int _laserCharges;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _cannonManager = GetComponent<CannonManager>();
            Extension.endOfGameEvent += ShowEndGamePanel;
        }

        private void FixedUpdate()
        {
            GetInfo();
            UpdateGuiInfo();
            UpdateScoreInfo();
        }

        // get information from player ship and cannon manager sources
        private void GetInfo()
        {
            _coordinates = _playerMovement.ShipTransform.position;
            _rotationAngle = _playerMovement.ShipTransform.rotation.eulerAngles.z;
            _instantSpeed = _playerMovement.ShipVelocity;
            _laserRechargeTime = _cannonManager.GetLaserRechargeTime;
            _laserCharges = _cannonManager.GetLaserCharges;
        }

        private void UpdateGuiInfo()
        {
            guiText.text = string.Format("COORDINATES: ({0:f2}, {1:f2}) \n", _coordinates.x, _coordinates.y) +
                           string.Format("ROTATION ANGLE: {0:f2} \n", _rotationAngle) +
                           string.Format("INSTANT SPEED: {0:f2} \n", _instantSpeed) +
                           string.Format("LASER CHARGES: {0} \n", _laserCharges) +
                           string.Format("LASER RECHARGE: {0:f2}", _laserRechargeTime);
        }
        
        private void UpdateScoreInfo() => scoreText.text = string.Format("SCORE: {0:}", Extension.score);
        
        private void ShowEndGamePanel()
        {
            endPanel.SetActive(true);
            endPanelScore.text = Extension.score.ToString();
        }

        private void OnDisable() => Extension.endOfGameEvent -= ShowEndGamePanel;
    }
}
