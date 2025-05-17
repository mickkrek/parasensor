using UnityEngine;

public class EquippedItemSlot : MonoBehaviour
{
    [SerializeField] private Item _defaultEmptyItem;
    void Start()
    {
        GameManager_Inventory.Instance.InventoryItemSelected.AddListener(ResetToDefault);
        GameManager_Inventory.Instance.InventoryItemPlaced.AddListener(CheckMyChildren);
        CheckMyChildren();
    }

    private void CheckMyChildren()
    {
        if (transform.GetChild(0).TryGetComponent(out InventoryItem myItem))
        {
            GameManager_Inventory.Instance.SelectedItemTitle.text = myItem.item.title;
            GameManager_Inventory.Instance.SelectedItemDescription.text = myItem.item.description;
            GameManager_Inventory.Instance.UpdateEquippedItem(myItem.item);
        }
        else
        {
            ResetToDefault();
        }
    }
    private void ResetToDefault()
    {
        GameManager_Inventory.Instance.SelectedItemTitle.text = _defaultEmptyItem.title;
        GameManager_Inventory.Instance.SelectedItemDescription.text = _defaultEmptyItem.description;
        GameManager_Inventory.Instance.UpdateEquippedItem(_defaultEmptyItem);
    }
}
