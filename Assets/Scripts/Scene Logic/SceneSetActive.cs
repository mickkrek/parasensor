using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSetActive : MonoBehaviour
{
    void Start()
    {
        SceneManager.SetActiveScene(this.gameObject.scene); //Set the newly loaded scene to active
    }
}
