using UnityEngine;
using Ghoulish.InteractionSystem;

public class Interactable_Object_Item : MonoBehaviour, IInteractable
{
    [SerializeField] private Item _item; 
    [SerializeField] private Transform _promptPosition;

    public void Start() 
    {
        //if player has already collected this item
        foreach(string s in GameManager.Instance.CollectedItems)
        {
            if (_item.title.Equals(s))
            {
                gameObject.SetActive(false);
                return;
            }
        }
    }

    public string GetPromptText()
    {
        return "Pick up " + _item.title;
    }

    public void Interact(Transform interactorTransform)
    {
        GameManager.Instance.CollectedItems.Add(_item.title);
        GameManager_Inventory.Instance.AddItem(_item);
        gameObject.SetActive(false);
    }

    public Vector3 GetPromptPosition()
    {
        return _promptPosition.position;
    }
}