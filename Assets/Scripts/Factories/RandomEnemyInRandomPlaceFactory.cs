using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace PocketZone.Factory
{
    public class RandomEnemyInRandomPlaceFactory
    {
        private const string _flesh = "Prefabs/Monsters/Flesh";
        private const string _zombie = "Prefabs/Monsters/Zombie";

        private readonly DiContainer _diContainer;

        private readonly List<Object> _spawnPrefab = new(2);

        public RandomEnemyInRandomPlaceFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Load()
        {
            _spawnPrefab.Add(Resources.Load(_flesh));
            _spawnPrefab.Add(Resources.Load(_zombie));
        }

        public void Create(int enemyCount, Vector2 startSpawnArea, Vector2 endSpawnArea)
        {
            var spawnArea = new Vector4(
                Mathf.Min(startSpawnArea.x, endSpawnArea.x),
                Mathf.Max(startSpawnArea.x, endSpawnArea.x),
                Mathf.Min(startSpawnArea.y, endSpawnArea.y),
                Mathf.Max(startSpawnArea.y, endSpawnArea.y));

            while (enemyCount > 0)
            {
                GameObject enemy = _diContainer.InstantiatePrefab(
                    _spawnPrefab[Random.Range(0, _spawnPrefab.Count)],
                    Vector3.zero,
                    Quaternion.identity,
                    null);

                var spawnPosition = new Vector2(
                    Random.Range(spawnArea.x, spawnArea.y),
                    Random.Range(spawnArea.z, spawnArea.w));

                while (Physics2D.OverlapBox(spawnPosition, enemy.GetComponent<BoxCollider2D>().size, 0) != null)
                {
                    spawnPosition = new Vector2(
                        Random.Range(spawnArea.x, spawnArea.y),
                        Random.Range(spawnArea.z, spawnArea.w));
                }

                enemy.transform.position = spawnPosition;
                enemyCount--;
            }
        }
    }
}