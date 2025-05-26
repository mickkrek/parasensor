using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Pixelplacement;

public class GameManager_GUI : MonoBehaviour
{
    #region Singleton
    private static GameManager_GUI _instance;

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
    public static GameManager_GUI Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameManager_GUI is NULL");
            return _instance;
        }
    }
    #endregion
    
    [Header("General")]
    [SerializeField] private Transform _mainCanvas = null;
    [SerializeField] private StateMachine _UIstateMachine;

    [Header("Conversation")]
    [SerializeField] private Transform _conversationParent = null;
    [SerializeField] private GameObject _speechBubblePrefab = null;
    
    [Header("Thoughts")]
    [SerializeField] private Transform _thoughtBubblePosition = null;
    [SerializeField] private Color _thoughtBubbleColor = Color.black;
    [SerializeField] private Color _thoughtTextColor = Color.white;

    public Transform MainCanvas => _mainCanvas;
    public Transform ConversationParent => _conversationParent;
    public Transform ThoughtBubblePosition => _thoughtBubblePosition;
    public GameObject SpeechBubblePrefab => _speechBubblePrefab;
    public Color ThoughtBubbleColor => _thoughtBubbleColor;
    public Color ThoughtTextColor => _thoughtTextColor;
    public StateMachine UIStateMachine => _UIstateMachine;

    public Transform SpeechBubbleTarget;
    public Image[] SpeechBubble_BG;
    public TextMeshProUGUI[] SpeechBubble_Text;

    public Transform ChatboxParent;


    public CanvasGroup PauseMenu;
    [HideInInspector] public bool PauseOpen;
}