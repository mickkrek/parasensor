using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Pixelplacement;

public class MainMenu_StartGame : MonoBehaviour
{
    [SerializeField] private Image Background;
    [SerializeField] private RectTransform FadeBlack, ForewordParent;
    [SerializeField] private CanvasGroup _menuGroup, _ghoulishGroup,_vicScreenGroup;

    [SerializeField] private GameObject _defaultButton;
    private float _delay;

    public string StartScene;
    public void Intro()
    {
        ForewordParent.gameObject.SetActive(false);
        _menuGroup.interactable = false;
        _menuGroup.alpha = 0f;
        FadeBlack.GetComponent<Image>().color = new Color(0,0,0,1);
        _delay += 2f;
        Tween.CanvasGroupAlpha(_ghoulishGroup, 1f, 1f, _delay);
        _delay += 2f;
        Tween.CanvasGroupAlpha(_ghoulishGroup, 0f, 1f, 4f);
        _delay += 1.5f;
        Tween.CanvasGroupAlpha(_vicScreenGroup, 1f, 1f, 5.5f);
        _delay += 2f;
        Tween.CanvasGroupAlpha(_vicScreenGroup, 0f, 1f, 7.5f, Tween.EaseLinear, Tween.LoopType.None, null, MainMenuIntro);
    }
    private void MainMenuIntro()
    {
        FadeBlack.GetComponent<Image>().color = new Color(0,0,0,0);
        Background.materialForRendering.SetFloat("_Fade", 0f);
        Background.materialForRendering.SetFloat("_Flash", 0f);
        Tween.ShaderFloat(Background.materialForRendering, "_Fade", 1f, 5f, 0f, Tween.EaseIn, Tween.LoopType.None);
        Tween.CanvasGroupAlpha(_menuGroup, 1f, 2f, 5f, Tween.EaseIn, Tween.LoopType.None, null, EnableCanvasGroup);
    }
    public void NewGame()
    {
        _menuGroup.interactable = false;
        Tween.ShaderFloat(Background.materialForRendering, "_Flash", 1f, .3f, 0f, Tween.EaseOutBack, Tween.LoopType.None);
        Tween.Color(FadeBlack.GetComponent<Image>(), Color.black, 2f, .5f, Tween.EaseLinear, Tween.LoopType.None, null, LoadNewGame);
    }

    public void QuitGame()
    {
        _menuGroup.interactable = false;
        Tween.Color(FadeBlack.GetComponent<Image>(), Color.black, 2f, 0f, Tween.EaseLinear, Tween.LoopType.None, null, LoadQuitGame);
    }
    private void LoadNewGame() 
    {
        SceneManager.LoadScene(StartScene);
    }
    private void LoadQuitGame()
    {
        Application.Quit();
    }
    private void EnableCanvasGroup()
    {
        _menuGroup.interactable = true;
        EventSystem.current.SetSelectedGameObject(_defaultButton);
    }
}
