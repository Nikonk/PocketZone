using PocketZone.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PocketZone.UI.Inventory
{
    [RequireComponent(typeof(Button))]
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _itemCount;

        [SerializeField] private Button _deleteButton;

        private InventoryDisplay ParentDisplay { get; set; }

        private InventorySlot _assignedInventorySlot;

        private Button _button;

        public InventorySlotUI Initialize()
        {
            ClearSlot();

            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnSlotClick);

            ParentDisplay = GetComponentInParent<InventoryDisplay>();

            _deleteButton.onClick.AddListener(ClearSlot);

            return this;
        }

        public void Init(InventorySlot assignedSlot)
        {
            _assignedInventorySlot = assignedSlot;
            UpdateSlot(assignedSlot);
        }

        public void UpdateSlot(InventorySlot slot)
        {
            if (slot.ItemData != null)
            {
                _itemIcon.sprite = slot.ItemData.Icon;
                _itemIcon.color = Color.white;

                _itemCount.text = slot.StackSize > 1 ? slot.StackSize.ToString() : string.Empty;
            }
            else
            {
                ClearSlot();
            }
        }

        private void ClearSlot()
        {
            _assignedInventorySlot?.ClearSlot();
            _itemIcon.sprite = null;
            _itemIcon.color = Color.clear;
            _itemCount.text = string.Empty;

            _deleteButton.gameObject.SetActive(false);
        }

        private void OnSlotClick()
        {
            ParentDisplay.SlotClicked(this);

            if (_assignedInventorySlot.StackSize != -1)
                _deleteButton.gameObject.SetActive(!_deleteButton.IsActive());
        }
    }
}