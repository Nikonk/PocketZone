using System;
using UnityEngine;

namespace PocketZone.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int _health;

        [SerializeField] private Player.Player _target;

        public Player.Player Target => _target;

        public void TakeDamage(int damage)
        {
            _health -= damage;

            if (_health <= 0)
                GenerateLoot();
        }

        private void GenerateLoot()
        {
            throw new NotImplementedException();
        }
    }
}