using Ink.Runtime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class InkTestingScript : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private SpeechBubble speechBubble_NPC, speechBubble_Marisol;
    [SerializeField] private Button buttonPrefab;
    private Story inkStory;
    private string currentSpeaker = "";
    void Start()
    {
        inkStory = new Story(inkJSON.text);
        inkStory.BindExternalFunction("SetCharacterState", (string characterCodeName, int state) =>{
            SetCharacterState(characterCodeName,state);
        });
        RefreshUI();
    }
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
    private void RefreshUI()
    {
        DestroyChildButtons();
        SpeechBubble storyTextObject = Instantiate(speechBubble_NPC);
        storyTextObject.transform.SetParent(transform, false);
        string loadedText = loadStoryLine();

        List<string> tags = inkStory.currentTags;
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
        }
        else
        {
            //Search through all characters in the list and find the corresponding display information
            string currentCodeName = GetStringBeforeKey(loadedText);
            CharacterState selectedCharacterState = GetCharacterState(currentCodeName);

            string speakerName = selectedCharacterState.displayName;
            storyTextObject.characterName.text  = selectedCharacterState.displayName;

            if (currentSpeaker != speakerName) //if the current speaker has changed
            {
                ShrinkAllBubbleIcons();
                storyTextObject.characterPortrait.gameObject.SetActive(true);
                storyTextObject.SetIconScale(1f);
                storyTextObject.characterPortrait.sprite = selectedCharacterState.icon;
                currentSpeaker = speakerName;
            }
            else
            {
                storyTextObject.characterPortrait.gameObject.SetActive(false);
                storyTextObject.characterName.gameObject.SetActive(false);
            }

            loadedText = textWithoutSpeakerName;
            storyTextObject.speechText.text = loadedText;
        }
        
        if (inkStory.currentChoices.Count == 0)
        {
            Button choiceButton = Instantiate(buttonPrefab);
            choiceButton.transform.SetParent(transform, false);
            TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = "Continue";
            choiceButton.onClick.AddListener(delegate{
                RefreshUI();
            });
            return;
        }
        foreach(Choice choice in inkStory.currentChoices)
        {
            Button choiceButton = Instantiate(buttonPrefab);
            choiceButton.transform.SetParent(transform, false);
            TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = choice.text;
            choiceButton.onClick.AddListener(delegate{
                ChooseStoryChoice(choice);
            });
        }
    }
    private void RefreshUIChoiceMade()
    {
        DestroyChildButtons();
        SpeechBubble storyTextObject = Instantiate(speechBubble_Marisol);
        storyTextObject.transform.SetParent(transform, false);
        string loadedText = loadStoryLine();
        currentSpeaker = "Marisol";
        storyTextObject.characterName.text = currentSpeaker;
        storyTextObject.characterPortrait.gameObject.SetActive(true);
        storyTextObject.speechText.text = loadedText;
        ShrinkAllBubbleIcons();
        storyTextObject.SetIconScale(1f);
        RefreshUI();
    }

    private void DestroyChildButtons()
    {
        foreach(Transform child in transform)
        {
            if (child.GetComponent<Button>())
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void ShrinkAllBubbleIcons()
    {
        foreach(Transform child in transform)
        {
            SpeechBubble childBubble = child.GetComponent<SpeechBubble>();
            if (childBubble != null)
            {
                childBubble.SetIconScale(0.75f);
            }
        }
    }

    private void ChooseStoryChoice(Choice selectedChoice)
    {
        inkStory.ChooseChoiceIndex(selectedChoice.index);
        RefreshUIChoiceMade();
    }

    private string loadStoryChunk()
    {
        string text = "";
        if (inkStory.canContinue)
        {
            text = inkStory.ContinueMaximally();
        }
        return text; 
    }
    private string loadStoryLine()
    {
        string text = "";
        if (inkStory.canContinue)
        {
            text = inkStory.Continue();
        }
        return text; 
    }
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
}
