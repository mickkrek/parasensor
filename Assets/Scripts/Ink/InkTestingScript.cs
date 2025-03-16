using Ink.Runtime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class InkTestingScript : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private Button buttonPrefab;
    private Story inkStory;
    void Start()
    {
        inkStory = new Story(inkJSON.text);
        RefreshUI();
    }
    private void RefreshUI()
    {
        //EraseUI();
        DestroyChildButtons();
        GameObject storyTextObject = Instantiate(textPrefab);
        storyTextObject.transform.SetParent(transform, false);
        TextMeshProUGUI storyText =  storyTextObject.GetComponentInChildren<TextMeshProUGUI>();
        string loadedText = loadStoryLine();

        List<string> tags = inkStory.currentTags;
        if (tags.Count > 0)
        {
            //handle tags here
            //loadedText = tags[0] + ": " + loadedText;
        }
        
        
        string textWithoutCharacterName = GetStringAfterKey(loadedText);
        if (textWithoutCharacterName != String.Empty)
        {
           loadedText = textWithoutCharacterName;
        }
        
        storyText.text = loadedText;
        if (inkStory.currentChoices.Count == 0)
        {
            Button choiceButton = Instantiate(buttonPrefab);
            choiceButton.transform.SetParent(transform, false);
            TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = "Continue";
            choiceButton.onClick.AddListener(delegate{
                ContinueLine();
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

    private void EraseUI()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
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

    private void ChooseStoryChoice(Choice selectedChoice)
    {
        inkStory.ChooseChoiceIndex(selectedChoice.index);
        RefreshUI();
    }

    private void ContinueLine()
    {
        RefreshUI();
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
                return text.Substring(start,text.Length-start);
            }
        }
        return String.Empty;
    }
}
