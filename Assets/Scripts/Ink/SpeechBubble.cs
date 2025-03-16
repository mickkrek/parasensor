using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    public TextMeshProUGUI characterName, speechText;
    public Image characterPortrait;
    private RectTransform characterPortraitScaleParent;

    private Vector3 originalIconScale;

    private void OnEnable()
    {
        characterPortraitScaleParent = characterPortrait.transform.parent.GetComponent<RectTransform>();
        originalIconScale = characterPortraitScaleParent.localScale;
    }
    public void SetIconScale(float scaleMultiplier)
    {
        characterPortraitScaleParent.localScale = originalIconScale * scaleMultiplier;
    }
}
