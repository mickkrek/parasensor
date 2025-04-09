using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSelect : MonoBehaviour
{
    [SerializeField] private string[] _scenes;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private StateMachineController stateMachineController;

    void Start()
    {
        foreach(string scene in _scenes)
        {
            GameObject go = Instantiate<GameObject>(buttonPrefab);
            go.transform.SetParent(this.transform);
            go.GetComponentInChildren<TextMeshProUGUI>().text = scene;
            go.GetComponent<Button>().onClick.AddListener(delegate { ChangeScene(scene); });
        }
    }
      
    public void ChangeScene(string sceneName)
    {
        GameManager_GUI.Instance.UIStateMachine.ChangeState("FadeIn");
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        }
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneAt(1)); //Set the newly loaded scene to active
    }
}
