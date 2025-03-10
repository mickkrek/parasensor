using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ghoulish.UISystem
{
    public class EmptySlot : MonoBehaviour
    {
        private UISelectableBase selectable;
        void Start()
        {
            selectable = GetComponent<UISelectableBase>();
            InventoryManager.Instance.InventoryItemSelected.AddListener(ShowSelf);
            InventoryManager.Instance.InventoryItemPlaced.AddListener(HideSelf);
        }
        public void MoveItem()
        {
            if (InventoryManager.Instance.SelectedInventoryItem != null)
            {
                InventoryManager.Instance.SelectedInventoryItem.Select();
                InventoryManager.Instance.InventoryItemPlaced.Invoke();
                int originalIndex = transform.GetSiblingIndex();
                int targetIndex = InventoryManager.Instance.SelectedInventoryItem.transform.GetSiblingIndex();
                transform.SetSiblingIndex(targetIndex);
                InventoryManager.Instance.SelectedInventoryItem.transform.SetSiblingIndex(originalIndex);
            }
        }

        private void ShowSelf()
        {
            selectable.ToggleInteractable(true);
        }
        private void HideSelf()
        {
            selectable.ToggleInteractable(false);
        }
        
    }
}