using UnityEngine;
using TMPro;

namespace Ghoulish.FixedCameraSystem
{
    public class GUI_ZoneEntryPrompt : MonoBehaviour
    {
        [SerializeField] private Transform _canvas;
        [SerializeField] private Transform _panelParent;
        [SerializeField] private TextMeshProUGUI _textTMPGUI;
        [SerializeField] private RectTransform _spritePanel;
        [SerializeField] private LayerMask _zoneLayerMask = default;
        private Vector2 _textSize;

        private CharacterController _charController;

        private void Update()
        {
            _charController = CameraManager.Instance.PlayerInput.charController;
            ZoneEntryPoint zoneEntryPrompt = GetZoneEntryPrompt();
            if (zoneEntryPrompt != null)
            {
                Show(zoneEntryPrompt.ZoneName);
                Vector3 followPos = zoneEntryPrompt.ZoneCollider.bounds.ClosestPoint(_charController.bounds.center);
                FollowPosition(followPos);
            }
            else
            {
                Hide();
            }
        }
        private ZoneEntryPoint GetZoneEntryPrompt()
        {
            Vector3 colliderPosition = _charController.bounds.center + (_charController.transform.forward * 0.5f);
            Collider[] colliderArray = Physics.OverlapSphere(colliderPosition, 1f, _zoneLayerMask);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out ZoneEntryPoint target))
                {
                    if (!target.Active) {

                        Vector3 dirToCollider = (target.ZoneCollider.bounds.ClosestPoint(_charController.bounds.center) - _charController.bounds.center).normalized;
                        Debug.DrawRay(_charController.bounds.center, dirToCollider * 1.5f, Color.yellow);
                        if (!Physics.Raycast(_charController.bounds.center, dirToCollider, 1.5f, ~0, QueryTriggerInteraction.Ignore))
                            return target;
                    }
                }
            }
            return null;
        }

        private void Show(string name)
        {
            _canvas.gameObject.SetActive(true);
            Setup(name);
        }

        private void Hide()
        {
            if (_canvas.gameObject.activeSelf) {
                _canvas.gameObject.SetActive(false);
            }
        }

        private void Setup(string text)
        {
            _textTMPGUI.SetText(text);
            _textTMPGUI.ForceMeshUpdate();
            _textTMPGUI.rectTransform.sizeDelta = _textSize;
            Vector2 padding = new Vector2(50f, 50f);
            _textSize = _textTMPGUI.GetRenderedValues(false);
            _spritePanel.sizeDelta = _textSize + padding;
        }

        private void FollowPosition(Vector3 pos)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(pos);
            screenPosition = new Vector3(Mathf.Clamp(screenPosition.x, 50f, Screen.width - 50f),Mathf.Clamp(screenPosition.y, 50f, Screen.height - 50f), screenPosition.z);
            _panelParent.position = screenPosition;
        }
    }
}
