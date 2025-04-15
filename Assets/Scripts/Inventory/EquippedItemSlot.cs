using UnityEngine;

public class EquippedItemSlot : MonoBehaviour
{
    void Start()
    {
        GameManager_Inventory.Instance.InventoryItemSelected.AddListener(RemoveDisplayText);
        GameManager_Inventory.Instance.InventoryItemPlaced.AddListener(CheckMyChildren);
        CheckMyChildren();
    }

    void CheckMyChildren()
    {
        if (transform.GetChild(0).TryGetComponent(out InventoryItem myItem))
        {
            GameManager_Inventory.Instance.SelectedItemTitle.text = myItem.item.title;
            GameManager_Inventory.Instance.SelectedItemDescription.text = myItem.item.description;
            GameManager_Inventory.Instance.UpdateEquippedItem(myItem.item);
        }
        else
        {
            RemoveDisplayText();
            return;
        }
        
    }
    void RemoveDisplayText()
    {
        GameManager_Inventory.Instance.SelectedItemTitle.text = "No item selected";
        GameManager_Inventory.Instance.SelectedItemDescription.text = "Place an item here to view its description.";
        GameManager_Inventory.Instance.UpdateEquippedItem(null);
    }
}
