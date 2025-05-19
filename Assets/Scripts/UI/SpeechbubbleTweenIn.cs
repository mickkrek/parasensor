using System.Drawing;
using Pixelplacement;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SpeechbubbleTweenIn : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 0.5f;
    [SerializeField] private TextMeshProUGUI[] _text;
    [SerializeField] private CanvasGroup _characterIcon;
    private CanvasGroup _canvasGroup;

    private Vector3 _characterIconPos;

    void OnEnable()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        transform.localScale = new Vector3(1f,0.8f,1f);
        if (_characterIcon != null)
        {
            _characterIcon.alpha = 0f;
            _characterIconPos = _characterIcon.transform.localPosition;
            _characterIcon.transform.localPosition = _characterIconPos + new Vector3(0f,-5f,0f);
        }
        SetTextAlpha(0f);
        FirstTween();
    }
    public void FadeIn()
    {
        
    }
    private void FirstTween()
    {
        Tween.CanvasGroupAlpha(_canvasGroup, 1f, _fadeDuration, 0f, Tween.EaseOut, Tween.LoopType.None, null, ReEnable);
        Tween.LocalScale(transform, new Vector3(1f,1f,1f), _fadeDuration/4f, 0f, Tween.EaseOutBack, Tween.LoopType.None, null, SecondTween);
    }
    private void SecondTween()
    {
        Tween.Value(0f, 1f, SetTextAlpha, _fadeDuration/2f, 0f, Tween.EaseOut, Tween.LoopType.None, null);
        if (_characterIcon != null)
        {
            Tween.CanvasGroupAlpha(_characterIcon, 1f, _fadeDuration/2f, 0f, Tween.EaseLinear, Tween.LoopType.None);
            Tween.LocalPosition(_characterIcon.transform, _characterIconPos, _fadeDuration/2f, 0f, Tween.EaseOutBack, Tween.LoopType.None, null);
        }
    }
    private void SetTextAlpha(float value)
    {
        foreach(TextMeshProUGUI tmp in _text)
        {
            tmp.alpha = value;
        }
    }
    private void ReEnable()
    {
        _canvasGroup.interactable = true;
    }
}
