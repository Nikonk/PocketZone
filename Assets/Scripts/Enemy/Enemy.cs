using PocketZone.LootGeneration;
using PocketZone.Unit;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace PocketZone.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private GameObject _selectionMarker;

        [FormerlySerializedAs("_heathBar")] [SerializeField] private FloatingHealthBar healthBar;

        private ILootGenerator _lootGenerator;

        private int _currentHealth;

        [Inject]
        private void Constructor(Player.Player player, ILootGenerator lootGenerator)
        {
            Target = player;
            _lootGenerator = lootGenerator;

            _currentHealth = _maxHealth;
            healthBar.UpdateHealthBar(_currentHealth, _maxHealth);
        }

        public Player.Player Target { get; private set; }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            healthBar.UpdateHealthBar(_currentHealth, _maxHealth);

            if (_currentHealth <= 0)
            {
                GenerateLoot();
                Destroy(gameObject);
            }
        }

        public void Select() => _selectionMarker.SetActive(true);
        public void Deselect() => _selectionMarker.SetActive(false);

        private void GenerateLoot() => _lootGenerator.Generate(transform.position);
    }
}