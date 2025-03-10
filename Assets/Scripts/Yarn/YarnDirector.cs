using System;
using UnityEngine;
using Yarn.Unity;

public class YarnDirector : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    [SerializeField] private YarnLineView yarnLineView;
    private SpeechBubbleMaker _previousBubbleMaker = null;

    private void Start() {
        // get handles of utility objects in the scene that we need
        dialogueRunner = GameObject.Find("Yarn System").GetComponent<DialogueRunner>();
        dialogueRunner.onDialogueStart.AddListener(OnDialogueStart);
        dialogueRunner.onDialogueComplete.AddListener(OnDialogueEnd);
        // <<SpeechBubble NAME_OF_CHARACTER>>
        dialogueRunner.AddCommandHandler<SpeechBubbleMaker>("SpeechBubble", UpdateSpeechBubble);
        // <<GiveItem NAME_OF_OBJECT>>
        dialogueRunner.AddCommandHandler<YarnGiveItem>("GiveItem", GiveItem);
    }

    private void OnDialogueStart() 
    {
        GameManager.Instance._conversationActive = true;
        GameManager.Instance.CharacterMovementEnabled(false);
    }
    private void OnDialogueEnd()
    {
        GameManager.Instance._conversationActive = false;
        GameManager.Instance.CharacterMovementEnabled(true);
    }
    // updates speechBubble to match the character speaking
    private void UpdateSpeechBubble(SpeechBubbleMaker bubbleMaker) 
    {
        bool isFirstLine = bubbleMaker != _previousBubbleMaker;
        yarnLineView.UpdateSpeechBubble(bubbleMaker.speechBubble.TextColor, bubbleMaker.speechBubble.BubbleColor, bubbleMaker.speechBubble.BubbleShape, isFirstLine);
        _previousBubbleMaker = bubbleMaker;
    }
    [YarnFunction]
    public static bool CheckInventory(string itemName)
    {
        InventoryItem[] items = GameManager_Inventory.Instance.SlotsParent.GetComponentsInChildren<InventoryItem>();
        int itemFound = 0;
        foreach(InventoryItem i in items)
        {
            if (i.item.title.Equals(itemName))
            {
                itemFound++;
            }
        }
        return itemFound > 0;
    }
    [YarnCommand("DestroyItem")]
    public static void DestroyItem(string itemName)
    {
        InventoryItem[] items = GameManager_Inventory.Instance.SlotsParent.GetComponentsInChildren<InventoryItem>();
        foreach(InventoryItem i in items)
        {
            if (i.item.title.Equals(itemName))
            {
                Destroy(i.gameObject);
                return;
            }
        }
    }
    [YarnCommand("EndGame")]
    public static void EndGame()
    {
        Application.Quit();
    }
    private void GiveItem(YarnGiveItem item)
    {
        GameObject.Find("GUI_NewJournalPrompt").GetComponent<GUI_NewItemPrompt>().TriggerPrompt();
        GameManager_Inventory.Instance.AddItem(item._item);
    }
}
