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

        public InventoryDisplay ParentDisplay { get; private set; }

        private InventorySlot _assignedInventorySlot;

        private Button _button;

        private void Awake()
        {
            ClearSlot();

            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnSlotClick);

            ParentDisplay = GetComponentInParent<InventoryDisplay>();

            _deleteButton.onClick.AddListener(ClearSlot);
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

        public void UpdateSlot()
        {
            if (_assignedInventorySlot != null)
                UpdateSlot(_assignedInventorySlot);
        }

        public void ClearSlot()
        {
            _assignedInventorySlot?.ClearSlot();
            _itemIcon.sprite = null;
            _itemIcon.color = Color.clear;
            _itemCount.text = string.Empty;

            _deleteButton.gameObject.SetActive(false);
        }

        public void OnSlotClick()
        {
            ParentDisplay.SlotClicked(this);

            if (_assignedInventorySlot.StackSize != -1)
                _deleteButton.gameObject.SetActive(!_deleteButton.IsActive());
        }
    }
}