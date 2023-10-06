using UnityEngine;
using Zenject;

namespace PocketZone.Inventory
{
    public class InventoryHolder : MonoBehaviour
    {
        public InventorySystem InventorySystem { get; private set; }

        [Inject]
        private void Constructor(InventorySystem inventorySystem)
        {
            InventorySystem = inventorySystem;
        }

        public bool TryAddToInventory(InventoryItemData itemToAdd, int amountToAdd) =>
            InventorySystem.TryAddToInventory(itemToAdd, amountToAdd);
    }
}