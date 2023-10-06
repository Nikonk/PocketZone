using PocketZone.Inventory;
using PocketZone.UI.Inventory;
using UnityEngine;
using Zenject;

namespace PocketZone.Installer
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerInitialPosition;

        [SerializeField] private PlayerInventoryDisplay _inventory;
        [SerializeField] private int _inventorySize;

        public override void InstallBindings()
        {
            BindInventorySize();
            BindPlayer();
        }

        private void BindInventorySize()
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
                .InstantiatePrefabForComponent<Player.Player>(_playerPrefab, _playerInitialPosition.position, Quaternion.identity, null)
                .Initialize(_inventory);

            Container
                .Bind<Player.Player>()
                .FromInstance(player)
                .AsSingle();
        }
    }
}