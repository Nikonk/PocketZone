using System;
using UnityEngine;

namespace PocketZone.Inventory.Item
{
    [Serializable]
    public class RandomItem : Item
    {
        [Range(0,1), SerializeField] private float _dropChance;
        [SerializeField] private int _dropCount;

        public float DropChance => _dropChance;
        public int DropCount => _dropCount;

        private void OnValidate()
        {
            if (_dropCount < 1)
                _dropCount = 1;
        }
    }
}