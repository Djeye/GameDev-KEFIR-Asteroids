using UnityEngine;
using UnityEngine.InputSystem;

namespace asteroids.SpaceShip
{
    public class PlayerInput : MonoBehaviour
    {
        // raw values of inputs for process them
        private Vector3 _rawMovementVector;
        private bool _rawAttackValue, _rawSpecialAttackValue;

        // event for visialize engine fire
        public delegate void ForwardButtonPressed(bool isPressed);
        public event ForwardButtonPressed buttonEvent;

        #region Input System methods
        public void OnMovement(InputAction.CallbackContext value)
        {
            Vector2 inputValue = value.ReadValue<Vector2>();
            _rawMovementVector = (Vector3)inputValue;
            buttonEvent.Invoke(_rawMovementVector.y > 0);
        }

        public void OnCommonAttack(InputAction.CallbackContext value)
        {
            _rawAttackValue = value.performed;
        }

        public void OnSpecialAttack(InputAction.CallbackContext value)
        {
            _rawSpecialAttackValue = value.performed;
        }
        #endregion

        #region Getters

        public Vector3 GetMovementVector() {
            return _rawMovementVector;
        }

        public bool GetCommonAttackValue()
        {
            return _rawAttackValue;
        }

        public bool GetSpecialAttackValue()
        {
            return _rawSpecialAttackValue;
        }
        #endregion
    }
}
