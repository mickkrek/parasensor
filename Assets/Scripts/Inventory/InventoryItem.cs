using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    private Image _icon;

    [HideInInspector] public Item item;

    public void InitialiseItem(Item newItem)
    {
        gameObject.name = new string(gameObject.name + "(" + item.title + ")");
        _icon = GetComponent<Image>();
        item = newItem;
        _icon.sprite = newItem.icon;
    }
}
