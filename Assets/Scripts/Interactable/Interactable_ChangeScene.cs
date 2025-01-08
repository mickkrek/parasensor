using UnityEngine;
using UnityEngine.SceneManagement;
using Ghoulish.InteractionSystem;
using Pixelplacement;

public class Interactable_ChangeScene : MonoBehaviour, IInteractable
{
    [SerializeField] private string _sceneName = "Scene File Name";
    [SerializeField] private string _name = "Scene Display Name";

    private CanvasGroup _mainGroup, _fadeBlack;
    private float _fadeDuration = .75f;

    void OnEnable()
    {
        _mainGroup = GameObject.Find("MainCanvas").GetComponent<CanvasGroup>();
        _fadeBlack = GameObject.Find("Fadeblack").GetComponent<CanvasGroup>();
    }
    public string GetPromptText()
    {
        return "Go to " + _name;
    }
    public void FadeOut()
    {
        GameManager.Instance.CharacterMovementEnabled(false);
        _mainGroup.interactable = false;
        _fadeBlack.alpha = 0f;
        Tween.CanvasGroupAlpha(_fadeBlack, 1f, _fadeDuration, 0f, Tween.EaseLinear, Tween.LoopType.None, ChangeScene);
    }
    private void ChangeScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
    public void Interact(Transform interactorTransform)
    {
        FadeOut();
    }

    public Vector3 GetPromptPosition()
    {
        throw new System.NotImplementedException();
    }
}
