using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Yarn.Unity;

public class GUI_SpeechBubble : MonoBehaviour
{
    [SerializeField] private RectTransform _spritePanel;
    [SerializeField] private TextMeshProUGUI _textTMPGUI;
    [SerializeField] private Transform _followPos;

    private void Update()
    {
        FollowPosition();
    }

    private void FollowPosition()
    {
        if (GameManager_GUI.Instance.SpeechBubbleTarget != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(GameManager_GUI.Instance.SpeechBubbleTarget.position);
            transform.position = screenPosition;
        }
    }

}
