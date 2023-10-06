using System;
using System.Collections.Generic;
using PocketZone.Inventory;
using PocketZone.UI.Inventory;
using UnityEngine;

namespace PocketZone.SaveLoad
{
    public class PlayerSaverLoader : IStorable
    {
        private const string _savePath = "/playerStats.json";
        private const string _inventoryItemDataResourcesPath = "Settings/ScriptableObjects/Item/";

        private readonly Player.Player _player;
        private readonly JsonDataService _dataService;

        public PlayerSaverLoader(Player.Player player)
        {
            _player = player;
            _dataService = new JsonDataService();
        }

        public void Save()
        {
            var inventorySlots = _player.GetFieldValue<InventoryHolder>("_inventory")
                .InventorySystem.GetFieldValue<List<InventorySlot>>("_inventorySlots");
            List<InventorySlotData> inventorySlotsData = new(inventorySlots.Count);

            foreach (InventorySlot inventorySlot in inventorySlots)
            {
                var inventorySlotData = new InventorySlotData()
                {
                    InventoryItemDataPath = inventorySlot.ItemData != null ? inventorySlot.ItemData.name : string.Empty,
                    StackSize = inventorySlot.StackSize
                };
                inventorySlotsData.Add(inventorySlotData);
            }

            var saveData = new PlayerData()
            {
                PositionX = _player.transform.position.x,
                PositionY = _player.transform.position.y,
                CurrentHealth = _player.GetFieldValue<int>("_currentHealth"),
                InventorySlotsData = inventorySlotsData
            };

            if (_dataService.Save(_savePath, saveData) == false)
                Debug.LogError($"{GetType()} cannot save player data");
        }

        public void Load()
        {
            PlayerData loadData;

            try
            {
                loadData = _dataService.Load<PlayerData>(_savePath);
            }
            catch (Exception)
            {
                Debug.LogError($"{GetType()} cannot load player data");
                return;
            }

            _player.transform.position = new Vector2(loadData.PositionX, loadData.PositionY);
            _player.SetFieldValue("_currentHealth", loadData.CurrentHealth);

            List<InventorySlotData> inventorySlotsData = loadData.InventorySlotsData;
            List<InventorySlot> inventorySlots = new(inventorySlotsData.Count);

            foreach (InventorySlotData inventorySlotData in inventorySlotsData)
            {
                InventoryItemData inventoryItemData;

                if (inventorySlotData.InventoryItemDataPath.Length != 0)
                    inventoryItemData =
                        Resources.Load<InventoryItemData>(_inventoryItemDataResourcesPath +
                                                          inventorySlotData.InventoryItemDataPath);
                else
                    inventoryItemData = null;

                inventorySlots.Add(new InventorySlot(inventoryItemData, inventorySlotData.StackSize));
            }

            InventorySystem inventorySystem = _player.GetFieldValue<InventoryHolder>("_inventory").InventorySystem;
            inventorySystem.SetFieldValue("_inventorySlots", inventorySlots);

            _player.GetFieldValue<InventoryDisplay>("_inventoryDisplay").AssignSlot(inventorySystem);
        }
    }
}