using UnityEngine;
using Ghoulish.InteractionSystem;

public class Interactable_LockedDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string _name, _sceneName, _yarnThoughtNode;
    [SerializeField] private Item _requiredItem;
    private Interactable_ChangeScene _changeScene;
    [SerializeField] private Transform _objectToEnable, _objectToDisable;
    private bool _locked = true;

    public void Start() 
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
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
                return;
            }
            else
            {
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
        InventoryItem[] items = GameManager_Inventory.Instance.SlotsParent.GetComponentsInChildren<InventoryItem>();
        foreach(InventoryItem i in items)
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
