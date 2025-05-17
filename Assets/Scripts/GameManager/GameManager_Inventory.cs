using Ghoulish.UISystem;
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
    public UnityEvent InventoryItemSelected, InventoryItemPlaced;

    public void Start()
    {
        _slots = SlotsParent.GetComponentsInChildren<InventorySlot>();
        InventoryItemSelected ??= new UnityEvent();
        InventoryItemPlaced ??= new UnityEvent();
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
    public void DestroyItem(string itemName)
    {
        InventoryItem[] items = SlotsParent.GetComponentsInChildren<InventoryItem>();
        foreach(InventoryItem i in items)
        {
            if (i.item.title.Equals(itemName))
            {
                Destroy(i.gameObject);
                return;
            }
        }
        Debug.Log("No such item found in inventory: " + itemName);
    }
}