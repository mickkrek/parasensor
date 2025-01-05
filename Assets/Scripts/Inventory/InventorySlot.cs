using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private Item _item;

    public void OnSelect(BaseEventData eventData)
    {
        InventoryItem inv = GetComponentInChildren<InventoryItem>();
        if (inv != null)
        {   _item = inv.item;
            GameManager_GUI.Instance.SelectedItemTitle.text = _item.title;
            GameManager_GUI.Instance.SelectedItemDescription.text = _item.description;
            GameManager_GUI.Instance.SelectedItemImage.sprite = _item.descriptionImage;
        }
    }

    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        GameManager_GUI.Instance.RevertInventoryToDefault();
    }
}