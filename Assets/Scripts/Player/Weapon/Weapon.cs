using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PocketZone.Player
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponData _weaponData;

        private int _currentAmmo;

        private float _timeSinceLastShot;
        private float _timeBetweenShots;

        private bool _isReloading;

        public WeaponData WeaponData => _weaponData;

        private void Start()
        {
            _timeBetweenShots = 1f / (_weaponData.FireRate / 60f);
            _currentAmmo = _weaponData.ClipSize;
        }

        private void Update()
        {
            _timeSinceLastShot += Time.deltaTime;
            Debug.Log(_currentAmmo);
        }

        public void Shoot(Enemy.Enemy enemy = null)
        {
            Debug.Log($"Shoot: {_currentAmmo}");

            if (_currentAmmo > 0 && CanShoot())
            {
                _currentAmmo--;

                if (enemy != null)
                    enemy.TakeDamage(_weaponData.Damage);
            }
        }

        public async UniTaskVoid Reload()
        {
            if (_isReloading)
                return;

            _isReloading = true;

            await UniTask.WaitForSeconds(_weaponData.ReloadTime);

            _currentAmmo = _weaponData.ClipSize;
            _isReloading = false;
        }

        private bool CanShoot() => _isReloading == false && _timeSinceLastShot > _timeBetweenShots;
    }
}