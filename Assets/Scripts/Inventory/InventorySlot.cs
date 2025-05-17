using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, ISelectHandler
{
    private Item _item;

    public void OnSelect(BaseEventData eventData)
    {
        InventoryItem inv = GetComponentInChildren<InventoryItem>();
        if (inv != null)
        {   
            _item = inv.item;
            GameManager_Inventory.Instance.SelectedItemTitle.text = _item.title;
            GameManager_Inventory.Instance.SelectedItemDescription.text = _item.description;
        }
    }
}