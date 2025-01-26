using UnityEngine;
using Yarn.Unity;
using Ghoulish.InteractionSystem;

public class Interactable_Object_Item : MonoBehaviour, IInteractable
{
    [SerializeField] private Item _item; 
    private DialogueRunner dialogueRunner;
    [SerializeField] private Transform _promptPosition;

    public void Start() 
    {
        dialogueRunner = GameObject.Find("Yarn System").GetComponent<DialogueRunner>();

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
        //dialogueRunner.StartDialogue(_item.yarnNodeTitle);
        GameManager.Instance.CollectedItems.Add(_item.title);
        GameManager_Inventory.Instance.AddItem(_item);
        gameObject.SetActive(false);
    }

    public Vector3 GetPromptPosition()
    {
        return _promptPosition.position;
    }
}