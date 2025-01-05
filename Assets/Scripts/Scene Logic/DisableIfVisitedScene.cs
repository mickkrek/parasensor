using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableIfVisitedScene : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance.VisitedScenes.Contains(SceneManager.GetActiveScene().name))
        {
            gameObject.SetActive(false);
        }
    }
}
