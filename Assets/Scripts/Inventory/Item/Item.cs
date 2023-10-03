using UnityEngine;

namespace PocketZone.Inventory.Item
{
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField] private string _name;

        public string Name => _name;
    }
}