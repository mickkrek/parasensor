using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_FadeIn : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameManager.Instance.UIStateMachine.ChangeState("FadeIn");
    }
}
