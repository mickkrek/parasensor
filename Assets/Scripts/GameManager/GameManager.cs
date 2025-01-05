using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    private void Awake()
    {
        if (_instance == null || _instance == this)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameManager is NULL");
            return _instance;
        }
    }
    #endregion
    
    private CharacterController _characterController = null;
    [SerializeField] private CharacterList _characterList;
    private static CharacterList _characterListInstance;
    
    public bool _characterMovementEnabled = true;
    [HideInInspector] public bool _conversationActive = false;
    [HideInInspector] public GameObject PlayerSystem = null;
    [HideInInspector] public CharacterController CharacterController => _characterController;
    [HideInInspector] public CharacterList CharacterListInstance => _characterListInstance;
    [HideInInspector] public Character ActiveCharacter;
    [HideInInspector] public List<string> VisitedScenes;
    [HideInInspector] public List<string> CollectedItems;
    private void OnEnable()
    {
        PlayerSystem = GameObject.Find("PlayerSystem");
        _characterController = PlayerSystem.GetComponentInChildren<CharacterController>();
        _characterListInstance = Instantiate(_characterList);//create a runtime instance of the character list
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    private void OnSceneUnloaded(Scene current)
    {
        if (!VisitedScenes.Contains(current.name))
        {
            VisitedScenes.Add(current.name);
        }
    }
}
