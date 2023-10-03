using PocketZone.LootGeneration;
using UnityEngine;
using Zenject;

namespace PocketZone.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private GameObject _selectionMarker;

        private ILootGenerator _lootGenerator;

        [Inject]
        private void Constructor(Player.Player player, ILootGenerator lootGenerator)
        {
            Target = player;
            _lootGenerator = lootGenerator;
        }

        public Player.Player Target { get; private set; }

        public void TakeDamage(int damage)
        {
            _health -= damage;

            if (_health <= 0)
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