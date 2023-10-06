using System;
using UnityEngine;

namespace PocketZone.Inventory
{
    [Serializable]
    public class InventorySlot
    {
        [SerializeField] private InventoryItemData _itemData;
        [SerializeField] private int _stackSize;

        public InventoryItemData ItemData => _itemData;
        public int StackSize => _stackSize;

        public InventorySlot(InventoryItemData source, int amount)
        {
            _itemData = source;
            _stackSize = amount;
        }

        public InventorySlot() => ClearSlot();

        public void ClearSlot()
        {
            _itemData = null;
            _stackSize = -1;
        }

        public void UpdateInventorySlot(InventoryItemData itemData, int amount)
        {
            _itemData = itemData;
            _stackSize = amount;
        }

        public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
        {
            amountRemaining = _itemData.MaxStack - _stackSize;
            return RoomLeftInStack(amountToAdd);
        }
        public bool RoomLeftInStack(int amountToAdd) => (_stackSize + amountToAdd) <= _itemData.MaxStack;

        public void AddToStack(int amount) => _stackSize += amount;
        public void RemoveFromStack(int amount) => _stackSize -= amount;
    }
}