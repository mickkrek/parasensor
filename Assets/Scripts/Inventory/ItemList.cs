using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemList", menuName = "Inventory/Create Item List", order = 2)]
public class ItemList : ScriptableObject
{
    public Item[] items;
}
