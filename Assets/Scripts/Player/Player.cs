using PocketZone.Input;
using UnityEngine;

namespace PocketZone.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private int _maxHealth;

        private PhoneInput _input;

        private PlayerMovement _playerMovement;
        private Vector2 _moveDirection;

        private int _currentHealth;

        private void Awake()
        {
            _input = new PhoneInput();

            _playerMovement = new PlayerMovement(GetComponent<Rigidbody2D>());

            _currentHealth = _maxHealth;
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
            _playerMovement.Move(_moveDirection.x, _moveDirection.y, _movementSpeed);
        }

        public void ApplyDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
                Destroy(gameObject);
        }
    }
}