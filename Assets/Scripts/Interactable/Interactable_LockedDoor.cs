using UnityEngine;
using Yarn.Unity;
using Ghoulish.InteractionSystem;

public class Interactable_LockedDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string _name, _sceneName, _yarnThoughtNode;
    [SerializeField] private Item _requiredItem;
    private Interactable_ChangeScene _changeScene;
    [SerializeField] private Transform _objectToEnable, _objectToDisable;
    private DialogueRunner dialogueRunner;
    private bool _locked = true;

    public void Start() 
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
        dialogueRunner = GameObject.Find("Yarn System").GetComponent<DialogueRunner>();
        _changeScene = GetComponentInChildren<Interactable_ChangeScene>();
        _changeScene.transform.gameObject.SetActive(false);
        _objectToEnable.gameObject.SetActive(false);
        _objectToDisable.gameObject.SetActive(true);
    }
    public string GetPromptText()
    {
        if (_locked)
        {
            if (!CheckInventory())
            {
                return "Inspect " + _name + " (locked)";
            }
            else
            {
                return "Unlock " + _name;
            }
        }
        return "Go to " + _name;
    }

    public void Interact(Transform interactorTransform)
    {
        if (_locked)
        {
            if (!CheckInventory())
            {
                dialogueRunner.StartDialogue(_yarnThoughtNode);
                return;
            }
            else
            {
                dialogueRunner.StartDialogue(_requiredItem.yarnNodeTitle);
                _objectToEnable.gameObject.SetActive(true);
                _objectToDisable.gameObject.SetActive(false);
                _locked = false;
                return;
            }
        }
        _changeScene.transform.gameObject.SetActive(true);
        _changeScene.FadeOut();
    }
    private bool CheckInventory()
    {
        InventoryItem[] trinketItems = GameManager_Inventory.Instance.TrinketSlotsParent.GetComponentsInChildren<InventoryItem>();
        foreach(InventoryItem i in trinketItems)
        {
            if (i.item.title.Equals(_requiredItem.title))
            {
                return true;
            }
        }
        InventoryItem[] toolItems = GameManager_Inventory.Instance.ToolSlotsParent.GetComponentsInChildren<InventoryItem>();
        foreach(InventoryItem i in toolItems)
        {
            if (i.item.title.Equals(_requiredItem.title))
            {
                return true;
            }
        }
        return false;
    }

    public Vector3 GetPromptPosition()
    {
        throw new System.NotImplementedException();
    }
}
