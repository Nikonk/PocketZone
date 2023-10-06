using System.Collections.Generic;
using PocketZone.Inventory;
using UnityEngine;

namespace PocketZone.UI.Inventory
{
    public class PlayerInventoryDisplay : InventoryDisplay
    {
        [SerializeField] private InventorySlotUI _inventorySlotUIPrefab;

        private InventoryHolder _playerInventory;
        private List<InventorySlotUI> _slots;

        public void Initialize(InventoryHolder playerInventory)
        {
            _playerInventory = playerInventory;
            InventorySystem = _playerInventory.InventorySystem;
            InventorySystem.OnInventorySlotChanged += UpdateSlot;

            _slots = new List<InventorySlotUI>(InventorySystem.InventorySize);

            for (int i = 0; i < InventorySystem.InventorySize; i++)
            {
                InventorySlotUI inventorySlotUI = Instantiate(_inventorySlotUIPrefab, transform).Initialize();
                _slots.Add(inventorySlotUI);
            }

            AssignSlot(InventorySystem);
        }

        public override void AssignSlot(InventorySystem inventoryToDisplay)
        {
            SlotsDictionary = new Dictionary<InventorySlotUI, InventorySlot>();

            for (int i = 0; i < InventorySystem.InventorySize; i++)
            {
                SlotsDictionary.Add(_slots[i], InventorySystem.InventorySlots[i]);
                _slots[i].Init(InventorySystem.InventorySlots[i]);
            }
        }
    }
}