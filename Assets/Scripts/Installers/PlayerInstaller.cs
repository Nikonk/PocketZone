using UnityEngine;
using Zenject;

namespace PocketZone.Installer
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerInitialPosition;

        public override void InstallBindings()
        {
            BindPlayer();
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