using System.Collections.Generic;
using PocketZone.Inventory;
using UnityEngine;

namespace PocketZone.UI.Inventory
{
    public abstract class InventoryDisplay : MonoBehaviour
    {
        protected InventorySystem InventorySystem;
        protected Dictionary<InventorySlotUI, InventorySlot> SlotsDictionary;

        public abstract void AssignSlot(InventorySystem inventoryToDisplay);

        protected virtual void UpdateSlot(InventorySlot newSlot)
        {
            foreach (var inventorySlot in SlotsDictionary)
                if (inventorySlot.Value == newSlot)
                    inventorySlot.Key.UpdateSlot(newSlot);
        }

        public void SlotClicked(InventorySlotUI clickedSlot)
        {
            Debug.Log("Slot clicked");
        }
    }
}