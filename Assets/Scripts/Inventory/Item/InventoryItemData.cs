using UnityEngine;

namespace PocketZone.Inventory
{
    [CreateAssetMenu(fileName = "InventoryItemData", menuName = "InventoryItemData")]
    public class InventoryItemData : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _maxStack;
        [SerializeField] private ItemType _itemType;

        public Sprite Icon => _icon;
        public int MaxStack => _maxStack;
        public ItemType ItemType => _itemType;
    }
}