using PocketZone.Inventory;
using UnityEngine;
using Zenject;

namespace PocketZone.Installer
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerInitialPosition;

        [SerializeField] private int _inventorySize;

        public override void InstallBindings()
        {
            BindInventory();
            BindPlayer();
        }

        private void BindInventory()
        {
            var inventorySystem = new InventorySystem(_inventorySize);

            Container
                .Bind<InventorySystem>()
                .FromInstance(inventorySystem)
                .AsSingle();
        }

        private void BindPlayer()
        {
            var player = Container
                .InstantiatePrefabForComponent<Player.Player>(_playerPrefab, _playerInitialPosition.position, Quaternion.identity, null);

            Container
                .Bind<Player.Player>()
                .FromInstance(player)
                .AsSingle();
        }
    }
}