using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Ghoulish.PlayerControls;
using Pixelplacement;
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
    [SerializeField] private CharacterList _characterList;
    private static CharacterList _characterListInstance;
    private IPlayerControls _playerControls;
    
    public bool _characterMovementEnabled = true;
    public StateMachine UIStateMachine;
    [HideInInspector] public bool _conversationActive = false;
    public CharacterController CharacterController;
    [HideInInspector] public CharacterList CharacterListInstance => _characterListInstance;
    [HideInInspector] public Character ActiveCharacter;
    [HideInInspector] public List<string> VisitedScenes;
    [HideInInspector] public List<string> CollectedItems;
    private void OnEnable()
    {
        _characterListInstance = Instantiate(_characterList);//create a runtime instance of the character list
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        _playerControls = CharacterController.GetComponent<IPlayerControls>();
    }
    private void OnSceneUnloaded(Scene current)
    {
        if (!VisitedScenes.Contains(current.name))
        {
            VisitedScenes.Add(current.name);
        }
    }
    public void CharacterMovementEnabled(bool enabled)
    {
        _characterMovementEnabled = enabled;
        _playerControls.MovementEnabled(enabled);
    }
    public void QuitApplication()
    {
        Application.Quit();
    }
}
