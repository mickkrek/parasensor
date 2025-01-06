using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    
    void Start()
    {
        _selectedItemImageDefault = _selectedItemImage.sprite;
        _selectedItemTitleDefault = _selectedItemTitle.text;
        _selectedItemDescriptionDefault = _selectedItemDescription.text;
    }
    public void RevertInventoryToDefault()
    {
        _selectedItemImage.sprite = _selectedItemImageDefault;
        _selectedItemTitle.text = _selectedItemTitleDefault;
        _selectedItemDescription.text = _selectedItemDescriptionDefault;
    }
    
    [Header("General")]
    [SerializeField] private Transform _mainCanvas = null;

    [Header("Inventory")]
    [SerializeField] private Transform _inventoryParent = null;
    [SerializeField] private TextMeshProUGUI _selectedItemTitle = null;
    [SerializeField] private TextMeshProUGUI _selectedItemDescription = null;
    [SerializeField] private Image _selectedItemImage = null;
    private Sprite _selectedItemImageDefault = null;
    private string _selectedItemTitleDefault = null;
    private string _selectedItemDescriptionDefault = null;
    public Transform OpenInventoryIcon, CloseInventoryIcon;

    [Header("Conversation")]
    [SerializeField] private Transform _conversationParent = null;
    [SerializeField] private GameObject _speechBubblePrefab = null;
    
    [Header("Thoughts")]
    [SerializeField] private Transform _thoughtBubblePosition = null;
    [SerializeField] private Color _thoughtBubbleColor = Color.black;
    [SerializeField] private Color _thoughtTextColor = Color.white;

    public Transform MainCanvas => _mainCanvas;
    public Transform ConversationParent => _conversationParent;
    public Transform InventoryParent => _inventoryParent;
    public Transform ThoughtBubblePosition => _thoughtBubblePosition;
    public GameObject SpeechBubblePrefab => _speechBubblePrefab;
    public TextMeshProUGUI SelectedItemTitle => _selectedItemTitle;
    public TextMeshProUGUI SelectedItemDescription => _selectedItemDescription;
    public Image SelectedItemImage => _selectedItemImage;
    public Color ThoughtBubbleColor => _thoughtBubbleColor;
    public Color ThoughtTextColor => _thoughtTextColor;

    public Transform SpeechBubbleTarget;
    public Image[] SpeechBubble_BG;
    public TextMeshProUGUI[] SpeechBubble_Text;

    public CanvasGroup PauseMenu;
    [HideInInspector] public bool PauseOpen;
}