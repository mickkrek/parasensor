using Pixelplacement;
using UnityEngine;

public class CanvasAlphaFadeOnEnable : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 0.5f;
    private CanvasGroup _canvasGroup;

    void OnEnable()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        FadeIn();
    }
    public void FadeIn()
    {
        Tween.CanvasGroupAlpha(_canvasGroup, 1f, _fadeDuration, 0f, Tween.EaseOut, Tween.LoopType.None, null, ReEnable);
    }
    private void ReEnable()
    {
        _canvasGroup.interactable = true;
    }
}
