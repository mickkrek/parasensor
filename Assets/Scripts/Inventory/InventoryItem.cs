using System.Collections;
using System.Collections.Generic;
using Ghoulish.UISystem;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    [HideInInspector] public Item item;

    public void InitialiseItem(Item newItem)
    {
        gameObject.name = new string(gameObject.name + "(" + newItem.title + ")");
        item = newItem;
        _image.sprite = newItem.descriptionImage;
    }
    public void SetSelectedToThis()
    {
        UISelectableBase thisSelectable = transform.gameObject.GetComponent<UISelectableBase>();
        GameManager_Inventory.Instance.SelectedInventoryItem = thisSelectable;
        GameManager_Inventory.Instance.InventoryItemSelected.Invoke();
    }
}
