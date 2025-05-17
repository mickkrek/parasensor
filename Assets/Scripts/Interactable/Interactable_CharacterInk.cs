using UnityEngine;
using Yarn.Unity;
using Ghoulish.InteractionSystem;

[RequireComponent(typeof(NPCEditorGizmo))]
public class Interactable_CharacterInk: MonoBehaviour, IInteractable
{
    [SerializeField] private Character _character;
    [SerializeField] private Transform _promptPosition;
    [SerializeField] private TextAsset _inkJSON;
    private InkDirector inkDirector;

    public void Start() 
    {
        inkDirector = FindFirstObjectByType<InkDirector>();
    }

    public string GetPromptText()
    {
        //This queries the global list of character trackers, checks for if one of them aligns with this character, then updates the prompt title based on the unlock status
        CharacterList CharList = GameManager.Instance.CharacterListInstance;
        for (int i=0; i < CharList.characterTrackers.Length;i++)
        {
            if (_character == CharList.characterTrackers[i].character)
            {
                if (string.IsNullOrEmpty(_character.codeName + "_Start"))
                {
                    return "Could not find accompanying yarn node";
                }
                return "Speak to " + _character.characterState[CharList.characterTrackers[i].currentState].displayName;
            }
        } 
        return "Could not find Character info in CharacterTracker";
    }

    private void StartConversation() 
    {
        CharacterList CharList = GameManager.Instance.CharacterListInstance;
        Debug.Log("Start Conversation");
        for (int i=0; i < CharList.characterTrackers.Length;i++)
        {
            if (_character == CharList.characterTrackers[i].character)
            {
                GameManager.Instance.ActiveCharacter = _character;
                GameManager_GUI.Instance.UIStateMachine.ChangeState("Conversation");
                inkDirector.LoadNewInkStory(_inkJSON);
            }
        } 
    }

    public void Interact(Transform interactorTransform)
    {
        Debug.Log("Interact");
        StartConversation();
        GameManager.Instance.CharacterMovementEnabled(false);
    }

    public Vector3 GetPromptPosition()
    {
        return _promptPosition.position;
    }
}
