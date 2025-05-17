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
            GameManager_Inventory.Instance.InventoryItemSelected.AddListener(ShowSelf);
            GameManager_Inventory.Instance.InventoryItemPlaced.AddListener(HideSelf);
        }
        public void MoveItem()
        {
            if (GameManager_Inventory.Instance.SelectedInventoryItem != null)
            {
                GameManager_Inventory.Instance.SelectedInventoryItem.Select();
                Transform originalParent = transform.parent;
                //Move this empty slot to the selected item's previous slot:
                transform.SetParent(GameManager_Inventory.Instance.SelectedInventoryItem.transform.parent);
                transform.localPosition = new Vector3(0f,0f,0f);
                //Move the selected item to my previous slot:
                GameManager_Inventory.Instance.SelectedInventoryItem.transform.SetParent(originalParent);
                GameManager_Inventory.Instance.SelectedInventoryItem.transform.localPosition = new Vector3(0f,0f,0f);
                GameManager_Inventory.Instance.InventoryItemPlaced.Invoke();
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