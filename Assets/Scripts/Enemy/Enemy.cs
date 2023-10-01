using System;
using UnityEngine;
using Zenject;

namespace PocketZone.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int _health;

        [Inject]
        private void Constructor(Player.Player player)
        {
            Target = player;
        }

        public Player.Player Target { get; private set; }

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