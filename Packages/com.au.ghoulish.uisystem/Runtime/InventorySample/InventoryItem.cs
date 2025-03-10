using UnityEngine;
using UnityEngine.EventSystems;

namespace Ghoulish.UISystem
{
    public class InventoryItem : MonoBehaviour
    {
        public void DisableIfNotSelected()
        {
            /*
            if (EventSystem.current.currentSelectedGameObject != this.transform.gameObject)
            {
                this.transform.gameObject.GetComponent<UISelectableBase>().interactable = false;
                return;
            }
            */
            InventoryManager.Instance.SelectedInventoryItem = transform.gameObject.GetComponent<UISelectableBase>();
            InventoryManager.Instance.InventoryItemSelected.Invoke();
        }
    }
}
