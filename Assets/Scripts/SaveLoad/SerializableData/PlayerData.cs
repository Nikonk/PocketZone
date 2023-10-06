using System;
using System.Collections.Generic;

namespace PocketZone.SaveLoad
{
    [Serializable]
    public struct PlayerData
    {
        public float PositionX;
        public float PositionY;
        public int CurrentHealth;
        public List<InventorySlotData> InventorySlotsData;
    }

    [Serializable]
    public struct InventorySlotData
    {
        public string InventoryItemDataPath;

        public int StackSize;
    }
}