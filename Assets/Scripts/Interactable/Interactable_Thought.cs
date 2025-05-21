using UnityEngine;
using Yarn.Unity;
using Ghoulish.InteractionSystem;

[ExecuteInEditMode]
[RequireComponent(typeof(Collider))]
public class Interactable_Thought : MonoBehaviour, IInteractable
{
    [SerializeField] private string _yarnStartNode;
    [SerializeField] private string _name = "Object Name";
    [SerializeField] private bool _disableAfterReading = false;
    private DialogueRunner dialogueRunner;

    public void Start() 
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
        dialogueRunner = GameObject.Find("Yarn System").GetComponent<DialogueRunner>();
    }

    public string GetPromptText()
    {
        return "Inspect " + _name;
    }

    public void Interact()
    {
        dialogueRunner.StartDialogue(_yarnStartNode);
        if (_disableAfterReading)
        {
            gameObject.SetActive(false);
        }
    }

    public Vector3 GetPromptPosition()
    {
        throw new System.NotImplementedException();
    }
}