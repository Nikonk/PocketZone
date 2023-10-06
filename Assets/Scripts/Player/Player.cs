using PocketZone.Input;
using PocketZone.Inventory;
using PocketZone.UI.Inventory;
using PocketZone.Unit;
using UnityEngine;

namespace PocketZone.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private int _maxHealth;

        [SerializeField] private Transform _weaponParent;
        [SerializeField] private ShootCircle _shootCircle;
        [SerializeField] private Weapon _startWeapon;

        [SerializeField] private InventoryHolder _inventory;

        [SerializeField] private FloatingHealthBar _healthBar;

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
            _inventoryDisplay
                .Initialize(_inventory);
            return this;
        }

        private void Awake()
        {
            _input = new PhoneInput();

            _playerMovement = new PlayerMovement(GetComponent<Rigidbody2D>());

            Weapon weapon = Instantiate(_startWeapon, _weaponParent);
            _playerWeaponBehavior = new PlayerWeaponBehavior(weapon, _shootCircle, transform);

            _currentHealth = _maxHealth;
            _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
        }

        private void OnEnable()
        {
            _input.Enable();

            _input.Player.Shoot.performed += _ => OnShootPerformed();
            _input.Player.Shoot.canceled += _ => OnShootCanceled();

            _input.Player.Reload.performed += _ => OnReload();

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
            _healthBar.UpdateHealthBar(_currentHealth, _maxHealth);

            if (_currentHealth <= 0)
                Destroy(gameObject);
        }

        private void OnShootPerformed() => _isShooting = true;
        private void OnShootCanceled() => _isShooting = false;

        private void OnReload() => _playerWeaponBehavior.Reload(_inventory.InventorySystem.GetAmmo(_playerWeaponBehavior.WeaponAmmoType, _playerWeaponBehavior.ClipSize));

        private void OnOpenInventory() => _inventoryDisplay.gameObject.SetActive(!_inventoryDisplay.isActiveAndEnabled);
    }
}