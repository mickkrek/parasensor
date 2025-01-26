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
                GameManager_Inventory.Instance.InventoryItemPlaced.Invoke();
                Transform originalParent = transform.parent;
                transform.SetParent(GameManager_Inventory.Instance.SelectedInventoryItem.transform.parent);
                transform.localPosition = new Vector3(0f,0f,0f);
                GameManager_Inventory.Instance.SelectedInventoryItem.transform.SetParent(originalParent);
                GameManager_Inventory.Instance.SelectedInventoryItem.transform.localPosition = new Vector3(0f,0f,0f);
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