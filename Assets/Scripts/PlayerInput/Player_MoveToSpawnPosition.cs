using UnityEngine;
using UnityEngine.SceneManagement;
public class Player_MoveToSpawnTransform : MonoBehaviour
{
    private string _previousSceneID = null;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawnPosObject = GameObject.Find("DefaultSpawnPosition");
        if (spawnPosObject== null)
        {
            Debug.LogWarning("Spawn Position Object not found!");
            return;
        }
        SceneSpawnPositions spawnList = spawnPosObject.GetComponent<SceneSpawnPositions>();
        for(int i=0; i < spawnList.Spawns.Length;i++)
        {
            if (spawnList.Spawns[i].PreviousSceneName == _previousSceneID)
            {
                transform.SetPositionAndRotation(spawnList.Spawns[i].SpawnTransform.position, spawnList.Spawns[i].SpawnTransform.rotation);
                Physics.SyncTransforms();
                break;
            }
            else
            {
                if (Application.isEditor)
                {
                    Debug.LogWarning("Spawn position not found so moved the player to first spawn position in the list.");
                    transform.SetPositionAndRotation(spawnList.Spawns[0].SpawnTransform.position, spawnList.Spawns[0].SpawnTransform.rotation);
                    Physics.SyncTransforms();
                }
            }
        }
        _previousSceneID = SceneManager.GetActiveScene().name;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
