using UnityEngine;

namespace asteroids.SpaceShip
{
    public class Visualisation : MonoBehaviour
    {
        // object for visualisation
        [SerializeField] GameObject engineFire;
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.buttonEvent += DrawEngineFire;
        }

        // draw engine fire on change input values
        private void DrawEngineFire(bool isPressed) => engineFire.SetActive(isPressed);

        private void OnDisable() => _playerInput.buttonEvent -= DrawEngineFire;
    }
}