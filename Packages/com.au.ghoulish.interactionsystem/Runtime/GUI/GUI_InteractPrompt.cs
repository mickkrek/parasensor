using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ghoulish.InteractionSystem
{
    public class GUI_InteractPrompt : MonoBehaviour
    {
        [SerializeField] private Transform _followPos;
        [SerializeField] private GameObject _container;
        [SerializeField] private TextMeshProUGUI _textTMPGUI;
        [SerializeField] private RectTransform _spritePanel;
        [SerializeField] private HorizontalLayoutGroup _layoutGroup;
        private Vector2 _textSize;

        private void Update()
        {
            IInteractable interactable = InteractionManager.Instance.GetInteractable();
            if (interactable != null)
            {
                Show(interactable);
            }
            else
            {
                Hide();
            }
        }

        private void Show(IInteractable interactable)
        {
            _container.SetActive(true);
            Setup(interactable.GetPromptText());
            FollowPosition(interactable.GetPromptPosition());
        }

        private void Hide()
        {
            if (_container.activeSelf) {
                _container.SetActive(false);
            }
        }

        private void Setup(string text)
        {
            _textTMPGUI.SetText(text);
            _textTMPGUI.ForceMeshUpdate();
            _textSize = _textTMPGUI.GetRenderedValues(false);
            _textTMPGUI.rectTransform.sizeDelta = _textSize;
            
            _spritePanel.sizeDelta = new Vector2(_layoutGroup.padding.right +_layoutGroup.padding.left, 0f) + _textSize + new Vector2(75f,50f);
        }

        private void FollowPosition(Vector3 pos)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(pos);
            screenPosition = new Vector3(Mathf.Clamp(screenPosition.x, 50f, Screen.width - 50f),Mathf.Clamp(screenPosition.y, 50f, Screen.height - 50f), screenPosition.z);
            transform.position = screenPosition;
        }
    }
}