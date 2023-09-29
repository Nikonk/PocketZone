using System;
using PocketZone.Input;
using UnityEngine;

namespace PocketZone.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private PhoneInput _input;

        private PlayerMovement _playerMovement;
        private Vector2 _moveDirection;

        private void Awake()
        {
            _input = new PhoneInput();

            _playerMovement = new PlayerMovement(GetComponent<Rigidbody2D>());
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Update()
        {
            _moveDirection = _input.Player.Move.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            _playerMovement.Move(_moveDirection.x, _moveDirection.y, _speed);
        }
    }
}