using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    public TextMeshProUGUI characterName, speechText;
    public Image characterPortrait, frameRenderer, bubbleRenderer;
    [SerializeField] private Sprite _activeFrameSprite, _inactiveFrameSprite;
    private RectTransform _characterPortraitScaleParent;

    private void OnEnable()
    {
        _characterPortraitScaleParent = characterPortrait.transform.parent.GetComponent<RectTransform>();
        frameRenderer.sprite = _activeFrameSprite;
    }
    public void ExpandBubble()
    {
        _characterPortraitScaleParent.localScale = new Vector3(1f, 1f, 1f);
        frameRenderer.sprite = _activeFrameSprite;
        bubbleRenderer.CrossFadeAlpha(1f, 0.2f, true);
    }
    public void ShrinkBubble()
    {
        _characterPortraitScaleParent.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        frameRenderer.sprite = _inactiveFrameSprite;
        bubbleRenderer.CrossFadeAlpha(0.8f, 0.2f, true);
    }
}
