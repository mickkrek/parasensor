using Pixelplacement;
using UnityEngine;

public class FadeBlack : MonoBehaviour
{
    private float _delay = 1f;
    private float _fadeDuration = 0.5f;
    private CanvasGroup _canvasGroup;

    void OnEnable()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        FadeIn();
    }
    public void FadeIn()
    {
        GameManager.Instance.CharacterMovementEnabled(false);
        _canvasGroup.alpha = 1f;
        Tween.CanvasGroupAlpha(_canvasGroup, 0f, _fadeDuration, _delay, Tween.EaseLinear, Tween.LoopType.None, ReEnable);
    }
    public void FadeOut()
    {
        GameManager.Instance.CharacterMovementEnabled(false);
        _canvasGroup.alpha = 0f;
        Tween.CanvasGroupAlpha(_canvasGroup, 1f, _fadeDuration, _delay, Tween.EaseLinear, Tween.LoopType.None);
    }
    private void ReEnable()
    {
        GameManager.Instance.CharacterMovementEnabled(true);
        Debug.Log("Enabled");
    }
}
