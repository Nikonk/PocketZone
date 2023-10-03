using PocketZone.Inventory.Item;
using UnityEngine;
using Zenject;

namespace PocketZone.LootGeneration
{
    public class RandomLootGenerator : ILootGenerator
    {
        private readonly RandomItem[] _randomItems;

        [Inject]
        public RandomLootGenerator(RandomItem[] randomItems)
        {
            _randomItems = randomItems;
        }

        public void Generate(Vector2 at)
        {
            foreach (RandomItem randomItem in _randomItems)
            {
                if (Random.Range(0f, 1f) < randomItem.DropChance)
                    for (int i = 0; i < randomItem.DropCount; i++)
                        Object.Instantiate(randomItem, at, Quaternion.identity);
            }
        }
    }
}