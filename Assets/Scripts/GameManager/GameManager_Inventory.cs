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

    public void Start()
    {
        _slots = SlotsParent.GetComponentsInChildren<InventorySlot>();
        InventoryItemSelected ??= new UnityEvent();
        InventoryItemPlaced ??= new UnityEvent();
        EquippedItemChanged ??= new UnityEvent();
    }
    public void AddItem(Item item)
    {
        _slots = SlotsParent.GetComponentsInChildren<InventorySlot>();
        ref Transform parentToUse = ref SlotsParent; //default to tools slots
        ref GameObject slotPrefabToUse = ref inventorySlotPrefab;

        //Find an empty slot, instantiate an item there.
        for (int i = 0; i < _slots.Length; i++)
        {
            InventorySlot targetSlot = _slots[i];
            EmptySlot targetEmptySlot = targetSlot.GetComponentInChildren<EmptySlot>();
            if (targetEmptySlot != null)
            {
                InstantiateItem(item, targetSlot.transform);
                Destroy(targetEmptySlot.gameObject);
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
    public void UpdateEquippedItem(Item itemToEquip)
    {
        EquippedItem = itemToEquip;
        EquippedItemChanged.Invoke();
    }
}