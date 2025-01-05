using UnityEngine;
using UnityEngine.SceneManagement;
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

    public Transform ToolSlotsParent, TrinketSlotsParent;
    public Transform ToolsCategory, TrinketsCategory;

    private InventorySlot[] _toolSlots, _trinketSlots;

    public GameObject inventorySlotPrefabTrinkets, inventorySlotPrefabTools;
    public GameObject inventoryItemPrefab;

    public void Start()
    {
        _toolSlots = ToolSlotsParent.GetComponentsInChildren<InventorySlot>();
        _trinketSlots = TrinketSlotsParent.GetComponentsInChildren<InventorySlot>();
    }
    public void AddItem(Item item)
    {
        ref InventorySlot[] slotsToUse = ref _toolSlots;
        ref Transform parentToUse = ref ToolSlotsParent; //default to tools slots
        ref GameObject slotPrefabToUse = ref inventorySlotPrefabTrinkets;
        if (item.itemType == Item.ItemType.Tool) 
        { 
            slotsToUse = ref _toolSlots;
            parentToUse = ref ToolSlotsParent;
            slotPrefabToUse = ref inventorySlotPrefabTrinkets;
        } 
        else if (item.itemType == Item.ItemType.Trinket) 
        { 
            slotsToUse = ref _trinketSlots;
            parentToUse = ref TrinketSlotsParent;
            slotPrefabToUse = ref inventorySlotPrefabTools;
        }

        //Find an empty slot, instantiate an item there. If no available slots, create new slot and instantiate item there.
        for (int i = 0; i < slotsToUse.Length; i++)
        {
            InventorySlot slot = slotsToUse[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                InstantiateItem(item, slot);
                return;
            }
        }
        InstantiateItem(item, InstantiateSlot(parentToUse, slotPrefabToUse));
    }
    public void InstantiateItem(Item item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
    public InventorySlot InstantiateSlot(Transform slotsParent, GameObject slotPrefab)
    {
        GameObject newSlotGO = Instantiate(slotPrefab, slotsParent);
        return newSlotGO.GetComponent<InventorySlot>();
    }
}