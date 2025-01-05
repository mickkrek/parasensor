using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("GUI Properties")]
    public string title;
    public string yarnNodeTitle;
    public Sprite icon;
    public string description;
    public Sprite descriptionImage;

    [Header("Gameplay Properties")]
    public ItemType itemType;

    public enum ItemType
    {
        Tool,
        Trinket
    }
}
