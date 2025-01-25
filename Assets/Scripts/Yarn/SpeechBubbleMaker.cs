using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;

public class SpeechBubbleMaker : MonoBehaviour
{
    [System.Serializable]
    public struct SpeechBubble {
        public Transform BubbleTarget;
        public Color BubbleColor;
        public Sprite BubbleShape;
        public Color TextColor;
    }

    public SpeechBubble speechBubble;
}
