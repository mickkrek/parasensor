using UnityEngine;

public class SceneSpawnPositions : MonoBehaviour
{
    public SpawnPosition[] Spawns;

    [System.Serializable]
    public struct SpawnPosition
    {
        public string PreviousSceneName;
        public Transform SpawnTransform; 
    }
}
