using UnityEngine;
using Yarn.Unity;
using Ghoulish.InteractionSystem;

[RequireComponent(typeof(NPCEditorGizmo))]
public class Interactable_Character: MonoBehaviour, IInteractable
{
    [SerializeField] private Character _character;
    [SerializeField] private Transform _promptPosition;
    private DialogueRunner dialogueRunner;

     public void Start() 
     {
        dialogueRunner = GameObject.Find("Yarn System").GetComponent<DialogueRunner>();
     }

    public string GetPromptText()
    {
        //This queries the global list of character trackers, checks for if one of them aligns with this character, then updates the prompt title based on the unlock status
        CharacterList CharList = GameManager.Instance.CharacterListInstance;
        for (int i=0; i < CharList.CharacterTrackers.Length;i++)
        {
            if (_character == CharList.CharacterTrackers[i].CharacterInfo)
            {
                if (string.IsNullOrEmpty(_character.NickName + "_Start"))
                {
                    return _character.Names[CharList.CharacterTrackers[i].CharacterState] + " has no accompanying Yarn Node.";
                }
                return "Speak to " + _character.Names[CharList.CharacterTrackers[i].CharacterState];
            }
        } 
        return "Could not find Character info in CharacterTracker";
    }

    private void StartConversation() 
    {
        CharacterList CharList = GameManager.Instance.CharacterListInstance;
        Debug.Log("Start Conversation");
        for (int i=0; i < CharList.CharacterTrackers.Length;i++)
        {
            if (_character == CharList.CharacterTrackers[i].CharacterInfo)
            {
                GameManager.Instance.ActiveCharacter = _character;
                GameManager_GUI.Instance.UIStateMachine.ChangeState("Conversation");
                dialogueRunner.StartDialogue(GameManager.Instance.ActiveCharacter.NickName + "_Start");
            }
        } 
    }

    public void Interact(Transform interactorTransform)
    {
        Debug.Log("Interact");
        if (!dialogueRunner.IsDialogueRunning)
        {
            StartConversation();
            GameManager.Instance.CharacterMovementEnabled(false);
        }
    }

    public Vector3 GetPromptPosition()
    {
        return _promptPosition.position;
    }
}
