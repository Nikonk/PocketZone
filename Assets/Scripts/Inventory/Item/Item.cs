using UnityEngine;

namespace PocketZone.Inventory.Item
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private InventoryItemData _itemData;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.TryGetComponent(out InventoryHolder inventoryHolder))
                if (inventoryHolder.TryAddToInventory(_itemData, 1))
                    Destroy(gameObject);
        }
    }
}