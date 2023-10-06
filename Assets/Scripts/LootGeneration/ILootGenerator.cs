using UnityEngine;

namespace PocketZone.LootGeneration
{
    public interface ILootGenerator
    {
        public void Generate(Vector2 at);
    }
}