using UnityEngine;
using UnityEngine.Events;

namespace Ghoulish.UISystem
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        [HideInInspector] public UISelectableBase SelectedInventoryItem; 
        public UnityEvent InventoryItemSelected, InventoryItemPlaced;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }
        void Start()
        {
            InventoryItemSelected ??= new UnityEvent();
            InventoryItemPlaced ??= new UnityEvent();
        }
    }
}