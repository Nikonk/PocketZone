using PocketZone.Inventory;
using UnityEngine;

namespace PocketZone.Player
{
    public class PlayerWeaponBehavior
    {
        private readonly ShootCircle _playerShootCircle;
        private readonly Transform _playerTransform;

        private Weapon _weapon;

        private Enemy.Enemy _enemy;

        public PlayerWeaponBehavior(Weapon initialWeapon, ShootCircle playerShootCircle, Transform playerTransform)
        {
            _weapon = initialWeapon;
            _playerTransform = playerTransform;

            _playerShootCircle = playerShootCircle;
            ChangeShootingDistance();
        }

        public ItemType WeaponAmmoType => _weapon.WeaponData.AmmoType;
        public int ClipSize => _weapon.WeaponData.ClipSize;

        public void Shoot()
        {
            _weapon.Shoot(_enemy);
        }

        public void Reload(int reloadingAmmo)
        {
            _weapon.Reload(reloadingAmmo).Forget();
        }

        public void ChangeWeapon(Weapon weapon)
        {
            _weapon = weapon;
            ChangeShootingDistance();
        }

        public void SearchEnemy()
        {
            const int enemyLayerMask = 1 << 7;

            if (_enemy != null)
            {
                if (Vector2.Distance(_playerTransform.position, _enemy.transform.position) > _weapon.WeaponData.Range)
                {
                    _enemy.Deselect();
                    _enemy = null;
                }
                else
                {
                    return;
                }
            }

            Collider2D enemyCollider =
                Physics2D.OverlapCircle(_playerTransform.position, _weapon.WeaponData.Range, enemyLayerMask);

            if (enemyCollider != null)
            {
                enemyCollider.TryGetComponent(out _enemy);
                _enemy.Select();
            }
        }

        public void ChangeEnemy()
        {
            Collider2D[] enemiesCollider = new Collider2D[2];

            if (Physics2D.OverlapCircleNonAlloc(_playerTransform.position, _weapon.WeaponData.Range,
                    enemiesCollider) <= 1)
                return;

            foreach (Collider2D enemyCollider in enemiesCollider)
            {
                Enemy.Enemy enemy = enemyCollider.GetComponent<Enemy.Enemy>();

                if (ReferenceEquals(_enemy, enemy) == false)
                {
                    _enemy.Deselect();
                    _enemy = enemy;
                    _enemy.Select();
                    return;
                }
            }
        }

        private void ChangeShootingDistance()
        {
            _playerShootCircle.Radius = _weapon.WeaponData.Range;

            SearchEnemy();
        }
    }
}