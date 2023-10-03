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

            _playerShootCircle = playerShootCircle;
            ChangeShootingDistance();

            _playerTransform = playerTransform;
        }

        public void Shoot()
        {
            _weapon.Shoot(_enemy);
        }

        public void Reload()
        {
            _weapon.Reload().Forget();
        }

        public void ChangeWeapon(Weapon weapon)
        {
            _weapon = weapon;
            ChangeShootingDistance();
        }

        public void SearchEnemy()
        {
            if (_enemy != null)
            {
                if (Vector2.Distance(_playerTransform.position, _enemy.transform.position) > _playerShootCircle.Radius)
                {
                    _enemy.Deselect();
                    _enemy = null;
                }
                else
                {
                    return;
                }
            }

            if (Physics2D.OverlapCircle(_playerTransform.position, _playerShootCircle.Radius).TryGetComponent(out _enemy))
                _enemy.Select();
        }

        public void ChangeEnemy()
        {
            Collider2D[] enemiesCollider = new Collider2D[2];

            if (Physics2D.OverlapCircleNonAlloc(_playerTransform.position, _playerShootCircle.Radius,
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

            if (_enemy == null)
                return;

            if (Vector2.Distance(_playerTransform.position, _enemy.transform.position) > _playerShootCircle.Radius)
            {
                _enemy.Deselect();
                _enemy = null;
                SearchEnemy();
            }
        }
    }
}