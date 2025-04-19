using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("GUI Properties")]
    public string title;
    public string description;
    public Sprite descriptionImage;

    [Header("Graphics Properties")]
    public GameObject graphicsPrefab;
    public AnimationClip upperBodyAnimation;
    public ItemBoneParent itemBoneParent;

    public enum ItemBoneParent
    {
        //Reference GameManagerInventory for corresponding bone transforms
        Root, LeftHand, RightHand, Chest, Backpack
    }

    [Header("Gameplay Properties")]
    public ItemType itemType;

    public enum ItemType
    {
        Item, Clothing
    }
}
