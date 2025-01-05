using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Pixelplacement;

public class Scene_FadeIn : MonoBehaviour
{

    private CanvasGroup _mainGroup, _fadeBlack;
    private Image[] _loadingIcons;
    private TextMeshProUGUI[] _loadingTexts;
    private float _delay = 1f;
    private float _fadeDuration = 0.5f;

    void OnEnable()
    {
        _mainGroup = GameObject.Find("MainCanvas").GetComponent<CanvasGroup>();
        _fadeBlack = GameObject.Find("Fadeblack").GetComponent<CanvasGroup>();
        _loadingIcons = _fadeBlack.GetComponentsInChildren<Image>();
        _loadingTexts = _fadeBlack.GetComponentsInChildren<TextMeshProUGUI>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameManager.Instance._characterMovementEnabled = false;
        _mainGroup.interactable = false;
        _fadeBlack.alpha = 1f;
        Tween.CanvasGroupAlpha(_fadeBlack, 0f, _fadeDuration, _delay, Tween.EaseLinear, Tween.LoopType.None, EnableMainCanvas);
    }
    private void EnableMainCanvas()
    {
        _mainGroup.interactable = true;
        GameManager.Instance._characterMovementEnabled = true;
    }
}
