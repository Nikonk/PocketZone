using PocketZone.Factory;
using PocketZone.Inventory.Item;
using PocketZone.LootGeneration;
using UnityEngine;
using Zenject;

namespace PocketZone.Installer
{
    public class EnemyInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private int _enemyCount;
        [SerializeField] private Transform _startSpawnPosition;
        [SerializeField] private Transform _endSpawnPosition;

        [SerializeField] private RandomItem[] _lootsAfterDeath;

        public override void InstallBindings()
        {
            BindInstaller();
            BindLootGenerator();
            BindEnemyFactory();
        }

        public void Initialize()
        {
            var enemyFactory = Container.Resolve<RandomEnemyInRandomPlaceFactory>();
            enemyFactory.Load();
            enemyFactory.Create(_enemyCount, _startSpawnPosition.position, _endSpawnPosition.position);
        }

        private void BindInstaller()
        {
            Container
                .BindInterfacesTo<EnemyInstaller>()
                .FromInstance(this)
                .AsSingle();
        }

        private void BindLootGenerator()
        {
            Container
                .Bind<RandomItem[]>()
                .FromInstance(_lootsAfterDeath)
                .AsSingle();

            Container
                .BindInterfacesTo<RandomLootGenerator>()
                .AsSingle();
        }

        private void BindEnemyFactory()
        {
            Container
                .Bind<RandomEnemyInRandomPlaceFactory>()
                .AsSingle();
        }
    }
}