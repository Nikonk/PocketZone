using System;
using UnityEngine;

namespace PocketZone.Inventory.Item
{
    [Serializable]
    public class RandomItem : Item
    {
        [Range(0,1), SerializeField] private float _dropChance;

        public float DropChance => _dropChance;
    }
}