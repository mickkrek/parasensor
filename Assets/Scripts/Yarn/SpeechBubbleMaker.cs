using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeechBubbleMaker : MonoBehaviour
{
    [System.Serializable]
    struct SpeechBubble {
        public Transform BubbleTarget;
        public Color BubbleColor;
        public Sprite BubbleShape;
        public Color TextColor;
    }

    [SerializeField] private SpeechBubble _speechBubble;

    public void UpdateSpeechBubble() 
    {
        GameManager_GUI.Instance.SpeechBubbleTarget = _speechBubble.BubbleTarget;
        
        foreach(Image i in GameManager_GUI.Instance.SpeechBubble_BG){
            i.color = _speechBubble.BubbleColor;
            i.sprite = _speechBubble.BubbleShape;
        }
        foreach(TextMeshProUGUI i in GameManager_GUI.Instance.SpeechBubble_Text){
            i.color = _speechBubble.TextColor;
        }
    }
}
