using System;
using Ghoulish.UISystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
public class GameManager_Inventory : MonoBehaviour
{
    #region Singleton
    private static GameManager_Inventory _instance;

    private void Awake()
    {
        if (_instance == null || _instance == this)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static GameManager_Inventory Instance
    {
        get
        {
            if (_instance is null)
                Debug.LogError("GameManager_Inventory is NULL");
            return _instance;
        }
    }
    #endregion

    public Transform SlotsParent;

    private InventorySlot[] _slots;

    public GameObject inventorySlotPrefab;
    public GameObject inventoryItemPrefab;
    [HideInInspector] public UISelectableBase SelectedInventoryItem; 
    public UnityEvent InventoryItemSelected, InventoryItemPlaced, EquippedItemChanged;

    [HideInInspector] public Item EquippedItem;

    public TextMeshProUGUI SelectedItemTitle = null;
    public TextMeshProUGUI SelectedItemDescription = null;

    [SerializeField] private ItemBoneParent[] itemBoneParents;
    [Serializable]
    public class ItemBoneParent
    {
        public string name;
        public Transform bone;
    }
    public void Start()
    {
        _slots = SlotsParent.GetComponentsInChildren<InventorySlot>();
        InventoryItemSelected ??= new UnityEvent();
        InventoryItemPlaced ??= new UnityEvent();
        EquippedItemChanged ??= new UnityEvent();
    }
    public void AddInventoryItem(Item item)
    {
        _slots = SlotsParent.GetComponentsInChildren<InventorySlot>();
        ref Transform parentToUse = ref SlotsParent; //default to tools slots
        ref GameObject slotPrefabToUse = ref inventorySlotPrefab;

        if (_slots.Length < 1)
        {
            Debug.LogError("No empty inventory slots - Mickey make a UI prompt here!");
        }
        //Find an empty slot, instantiate an item there.
        for (int i = 0; i < _slots.Length; i++)
        {
            InventorySlot targetSlot = _slots[i];
            EmptySlot targetEmptySlot = targetSlot.GetComponentInChildren<EmptySlot>();
            if (targetEmptySlot != null)
            {
                InstantiateItem(item, targetSlot.transform);
                Destroy(targetEmptySlot.gameObject);
                GameObject.Find("GUI_NewJournalPrompt").GetComponent<GUI_NewItemPrompt>().TriggerPrompt();
                if (!GameManager.Instance.CollectedItems.Contains(item.title))
                {
                    GameManager.Instance.CollectedItems.Add(item.title);
                }
                return;
            }
        }
    }
    public void InstantiateItem(Item item, Transform heirarchyPosition)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, heirarchyPosition);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
    private void DestroyEquippedItemObjects()
    {
        for(int i = 0; i< itemBoneParents.Length; i++)
        {
            if (itemBoneParents[i].bone.childCount > 0)
            {
                foreach(Transform child in itemBoneParents[i].bone)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
    private void CreateEquippedItemObject(Item itemToEquip)
    {
        if (itemToEquip.graphicsPrefab == null)
        {
            return;
        }
        for(int i = 0; i< itemBoneParents.Length; i++)
        {
            if (itemToEquip.itemBoneParent.ToString() == itemBoneParents[i].name)
            {
                Transform targetParent = itemBoneParents[i].bone;
                Instantiate(itemToEquip.graphicsPrefab, targetParent);
                return;
            }
        }
        Debug.LogError("Could not find bone name. Has the list of bones been changed?");
    }
    private void ChangeAnimatorPose(Item itemToEquip)
    {
        if (itemToEquip.upperBodyAnimation != null)
        {
            GameManager.Instance.CharacterController.GetComponent<PlayerInput_Animation>().ChangeEquippedItemPose(itemToEquip.upperBodyAnimation);
        }
        else
        {
            GameManager.Instance.CharacterController.GetComponent<PlayerInput_Animation>().RemoveEquippedItemPose();
        }
    }
    public void UpdateEquippedItem(Item itemToEquip)
    {
        EquippedItem = itemToEquip;
        DestroyEquippedItemObjects();
        CreateEquippedItemObject(itemToEquip);
        ChangeAnimatorPose(itemToEquip);
        EquippedItemChanged.Invoke();
    }
}