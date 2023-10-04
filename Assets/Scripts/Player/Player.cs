using PocketZone.Input;
using PocketZone.UI.Inventory;
using UnityEngine;
using Zenject;

namespace PocketZone.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private int _maxHealth;

        [SerializeField] private Transform _weaponParent;

        [SerializeField] private ShootCircle _shootCircle;

        [SerializeField] private Weapon _startWeapon;

        private PhoneInput _input;

        private PlayerMovement _playerMovement;
        private Vector2 _moveDirection;

        private int _currentHealth;

        private PlayerWeaponBehavior _playerWeaponBehavior;
        private bool _isShooting;

        private PlayerInventoryDisplay _inventoryDisplay;

        public Player Initialize(PlayerInventoryDisplay inventoryDisplay)
        {
            _inventoryDisplay = inventoryDisplay;
            return this;
        }

        private void Awake()
        {
            _input = new PhoneInput();

            _playerMovement = new PlayerMovement(GetComponent<Rigidbody2D>());

            Weapon weapon = Instantiate(_startWeapon, _weaponParent);
            _playerWeaponBehavior = new PlayerWeaponBehavior(weapon, _shootCircle, transform);

            _currentHealth = _maxHealth;
        }

        private void OnEnable()
        {
            _input.Enable();

            _input.Player.Shoot.performed += _ => OnShootPerformed();
            _input.Player.Shoot.canceled += _ => OnShootCanceled();

            _input.Player.OpenCloseInventory.performed += _ => OnOpenInventory();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Update()
        {
            _moveDirection = _input.Player.Move.ReadValue<Vector2>();

            _playerWeaponBehavior.SearchEnemy();

            if (_isShooting)
                _playerWeaponBehavior.Shoot();
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

        private void OnShootPerformed() => _isShooting = true;
        private void OnShootCanceled() => _isShooting = false;

        private void OnOpenInventory() => _inventoryDisplay.gameObject.SetActive(!_inventoryDisplay.isActiveAndEnabled);
    }
}