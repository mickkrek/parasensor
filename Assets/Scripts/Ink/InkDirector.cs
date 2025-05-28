using Ink.Runtime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Ghoulish.UISystem;
using UnityEngine.EventSystems;

public class InkDirector : MonoBehaviour
{
    [SerializeField] private SpeechBubble _speechBubble_NPC, _speechBubble_Marisol;
    [SerializeField] private UISelectableBase _buttonPrefab;
    [SerializeField] private Transform _UIContentParent;
    [SerializeField] private TextAsset _globalInkJSON;
    private Story _currentInkStory;
    private string currentSpeaker = "";

    private void Start()
    {
        InkStorySetup();
    }
    private void InkStorySetup()
    {
        //Create the global ink story, used throughout the whole game. Then bind all your functions.
        _currentInkStory = new Story(_globalInkJSON.text);
        _currentInkStory.BindExternalFunction("SetCharacterState", (string characterCodeName, int state) =>
        {
            SetCharacterState(characterCodeName, state);
        });
        _currentInkStory.BindExternalFunction("GivePlayerItem", (string itemName) =>
        {
            GivePlayerItem(itemName);
        });
    }
    public void LoadInkKnot(string knotName)
    {
        DestroyAllChildren();
        _currentInkStory.ChoosePathString(knotName);
        RefreshUI();
    }
    private void GivePlayerItem(string itemName)
    {
        var allItems = GameManager.Instance.itemList.items;
        for (int i = 0; i < allItems.Length; i++)
        {
            if (allItems[i].name == itemName)
            {
                GameManager_Inventory.Instance.AddInventoryItem(allItems[i]);
                return;
            }
        }
        Debug.LogError("No such inventory item named: " + itemName);
    }
    public void RefreshUI()
    {
        DestroyChildButtons();
        LayoutRebuilder.ForceRebuildLayoutImmediate(_UIContentParent as RectTransform); //force layout groups to update immediately instead at end of frame. Destroy is also called at end of frame so this causes race conditions
        SpeechBubble storyTextObject = Instantiate(_speechBubble_NPC);
        storyTextObject.transform.SetParent(_UIContentParent, false);
        string loadedText = loadStoryLine();

        List<string> tags = _currentInkStory.currentTags;
        if (tags.Count > 0)
        {
            //handle tags here
            //loadedText = tags[0] + ": " + loadedText;
        }
        string textWithoutSpeakerName = GetStringAfterKey(loadedText);
        if (textWithoutSpeakerName == String.Empty) //check if no speaker name on this line
        {
            //just load the whole text with no speaker name
            storyTextObject.speechText.text = loadedText;
            storyTextObject.characterName.gameObject.SetActive(false);
            storyTextObject.characterPortrait.gameObject.SetActive(false);
            storyTextObject.frameRenderer.gameObject.SetActive(false);
        }
        else
        {
            //Search through all characters in the list and find the corresponding display information
            string currentCodeName = GetStringBeforeKey(loadedText);
            CharacterState selectedCharacterState = GetCharacterState(currentCodeName);

            string speakerName = selectedCharacterState.displayName;
            storyTextObject.characterName.text = selectedCharacterState.displayName;

            if (currentSpeaker != speakerName) //if the current speaker has changed
            {
                ShrinkAllBubbleIcons();
                storyTextObject.characterPortrait.gameObject.SetActive(true);
                storyTextObject.frameRenderer.gameObject.SetActive(true);
                storyTextObject.ExpandBubble();
                storyTextObject.characterPortrait.sprite = selectedCharacterState.icon;
                currentSpeaker = speakerName;
            }
            else
            {
                storyTextObject.characterPortrait.gameObject.SetActive(false);
                storyTextObject.frameRenderer.gameObject.SetActive(false);
                storyTextObject.characterName.gameObject.SetActive(false);
            }

            loadedText = textWithoutSpeakerName;
            storyTextObject.speechText.text = loadedText;
        }
        
        if (_currentInkStory.currentChoices.Count == 0)
        {
            CreateButtonContinue();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_UIContentParent as RectTransform);
            return;
        }
        foreach(Choice choice in _currentInkStory.currentChoices)
        {
            CreateButtonChoice(choice);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_UIContentParent as RectTransform);
        }
    }
    private void CreateButtonContinue()
    {
        //Create a new button:
        UISelectableBase choiceButton = Instantiate(_buttonPrefab);
        choiceButton.transform.SetParent(_UIContentParent, false);
        TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
        choiceText.text = "Continue";
        choiceButton.onClick.AddListener(delegate{
            RefreshUI();
            });
        choiceButton.ToggleInteractable(true); //Use this custom function for a delayed interactable enable. Prevents the user automatically hitting submit
    }
    private void CreateButtonChoice(Choice choice)
    {
        //Create a new button:
        UISelectableBase choiceButton = Instantiate(_buttonPrefab);
        choiceButton.transform.SetParent(_UIContentParent, false);
        TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
        choiceText.text = choice.text;
        choiceButton.onClick.AddListener(delegate{
            ChooseStoryChoice(choice);
            });
        choiceButton.ToggleInteractable(true); //Use this custom function for a delayed interactable enable. Prevents the user automatically hitting submit
    }
    private void RefreshUIChoiceMade()
    {
        DestroyChildButtons();
        LayoutRebuilder.ForceRebuildLayoutImmediate(_UIContentParent as RectTransform); //force layout groups to update immediately instead at end of frame. Destroy is also called at end of frame so this causes race conditions
        SpeechBubble storyTextObject = Instantiate(_speechBubble_Marisol);
        storyTextObject.transform.SetParent(_UIContentParent, false);
        string loadedText = loadStoryLine();

        List<string> tags = _currentInkStory.currentTags;
        if (tags.Count > 0)
        {
            //handle tags here
            //loadedText = tags[0] + ": " + loadedText;
        }
        //Search through all characters in the list and find the corresponding display information
        string currentCodeName = "MARISOL";
        CharacterState selectedCharacterState = GetCharacterState(currentCodeName);
        string speakerName = selectedCharacterState.displayName;
        currentSpeaker = speakerName;
        storyTextObject.speechText.text = loadedText;

        ShrinkAllBubbleIcons();
        storyTextObject.characterPortrait.gameObject.SetActive(true);
        storyTextObject.frameRenderer.gameObject.SetActive(true);
        storyTextObject.ExpandBubble();
        storyTextObject.characterPortrait.sprite = selectedCharacterState.icon;
        storyTextObject.characterName.text = selectedCharacterState.displayName;
        
        CreateButtonContinue();
        LayoutRebuilder.ForceRebuildLayoutImmediate(_UIContentParent as RectTransform);
        return;
    }
    #region UIVisuals
    private void DestroyChildButtons()
    {
        foreach(Transform child in _UIContentParent)
        {
            if (child.GetComponent<UISelectableBase>())
            {
                Destroy(child.gameObject);
            }
        }
    }
    private void DestroyAllChildren()
    {
        foreach(Transform child in _UIContentParent)
        {
            Destroy(child.gameObject);
        }
    }
    private void ShrinkAllBubbleIcons()
    {
        foreach (Transform child in _UIContentParent)
        {
            SpeechBubble childBubble = child.GetComponent<SpeechBubble>();
            if (childBubble != null)
            {
                childBubble.ShrinkBubble();
            }
        }
    }
    #endregion
    #region CharacterStates
    private void SetCharacterState(string characterCodeName, int state)
    {
        var trackers = GameManager.Instance.CharacterListInstance.characterTrackers;
        for (int i = 0; i < trackers.Length; i++)
        {
            if (trackers[i].character.codeName == characterCodeName)
            {
                trackers[i].currentState = state;
                Debug.Log(trackers[i].currentState);
            }
        }
    }
    private CharacterState GetCharacterState(string characterCodeName)
    {
        var trackers = GameManager.Instance.CharacterListInstance.characterTrackers;
        for (int i = 0; i < trackers.Length; i++)
        {
            if (trackers[i].character.codeName == characterCodeName)
            {
                Character c = trackers[i].character;
                return c.characterState[trackers[i].currentState];
            }
        }
        Debug.LogError("Could not find character CodeName: " + characterCodeName + " in the character list.");
        CharacterState nullChar = new CharacterState();
        nullChar.displayName = "CODENAME NOT FOUND!";
        return nullChar;
    }
    #endregion
    #region InkFunctions
    private void ChooseStoryChoice(Choice selectedChoice)
    {
        _currentInkStory.ChooseChoiceIndex(selectedChoice.index);
        RefreshUIChoiceMade();
    }

    private string loadStoryChunk()
    {
        if (_currentInkStory.canContinue)
        {
            string text = _currentInkStory.ContinueMaximally();
            return text;
        }
        else
        {
            //return to gameplay mode when no more text to display
            GameManager_GUI.Instance.UIStateMachine.ChangeState("Game");
            return "Ink story flow has ended.";
        }
    }
    private string loadStoryLine()
    {
        if (_currentInkStory.canContinue)
        {
            string text = _currentInkStory.Continue();
            return text;
        }
        else
        {
            //return to gameplay mode when no more text to display
            GameManager_GUI.Instance.UIStateMachine.ChangeState("Game");
            return "Ink story flow has ended.";
        }
    }
    #endregion
    #region StringTools
    private string GetStringAfterKey(string text, string key = ": ")
    {
        if (!String.IsNullOrWhiteSpace(text))
        {
            int charLocation = text.IndexOf(key, StringComparison.Ordinal);

            if (charLocation > 0)
            {
                int start = charLocation + key.Length;
                return text.Substring(start, text.Length-start);
            }
        }
        return String.Empty;
    }
    private string GetStringBeforeKey(string text, string key = ": ")
    {
        if (!String.IsNullOrWhiteSpace(text))
        {
            int charLocation = text.IndexOf(key, StringComparison.Ordinal);

            if (charLocation > 0)
            {
                return text.Substring(0, charLocation);
            }
        }
        return String.Empty;
    }
    #endregion
}
