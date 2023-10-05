using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PocketZone.Inventory
{
    [Serializable]
    public class InventorySystem
    {
        [SerializeField] private List<InventorySlot> _inventorySlots;

        public IReadOnlyList<InventorySlot> InventorySlots => _inventorySlots;
        public int InventorySize => _inventorySlots.Count;

        public event Action<InventorySlot> OnInventorySlotChanged;

        public InventorySystem(int size)
        {
            _inventorySlots = new List<InventorySlot>(size);

            for (int i = 0; i < size; i++)
                _inventorySlots.Add(new InventorySlot());
        }

        public bool TryAddToInventory(InventoryItemData itemToAdd, int amountToAdd)
        {
            if (ContainsItem(itemToAdd, out List<InventorySlot> inventorySlots))
            {
                foreach (InventorySlot inventorySlot in inventorySlots)
                {
                    if (inventorySlot.RoomLeftInStack(amountToAdd))
                    {
                        inventorySlot.AddToStack(amountToAdd);
                        OnInventorySlotChanged?.Invoke(inventorySlot);
                        return true;
                    }
                }
            }

            if (HasFreeSlot(out InventorySlot freeSlot))
            {
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                OnInventorySlotChanged?.Invoke(freeSlot);
                return true;
            }

            return false;
        }

        public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> inventorySlot)
        {
            inventorySlot = _inventorySlots
                .Where(inventorySlot => inventorySlot.ItemData == itemToAdd).ToList();

            return inventorySlot.Count > 0;
        }

        public bool HasFreeSlot(out InventorySlot freeSlot)
        {
            freeSlot = _inventorySlots.FirstOrDefault(inventorySlot => inventorySlot.ItemData == null);

            return freeSlot != null;
        }

        public int GetAmmo(ItemType ammoType, int needCount)
        {
            int count = 0;
            List<InventorySlot> inventorySlots = _inventorySlots
                .Where(inventorySlot => inventorySlot.ItemData != null && inventorySlot.ItemData.ItemType == ammoType).ToList();

            foreach (InventorySlot inventorySlot in inventorySlots)
                if (inventorySlot.StackSize <= needCount)
                {
                    count += inventorySlot.StackSize;
                    inventorySlot.ClearSlot();
                }
                else
                {
                    count = needCount;
                    break;
                }

            return count;
        }
    }
 }