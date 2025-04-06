using UnityEngine;

namespace Ghoulish.FixedCameraSystem
{
    public class ZoneGraphicsToggle : MonoBehaviour
    {
        private CameraSwitcher camSwitcher;
        public Transform spriteParent;
        [SerializeField] private Transform environmentParent;

        void OnEnable()
        {
            camSwitcher = GetComponentInChildren<CameraSwitcher>();
        }

        void Update()
        {
            if (CameraManager.Instance.ActiveVirtualCam == camSwitcher.ThisVirtualCam)
            {
                SetParents(true);
            }
            else if (CameraManager.Instance.ActiveVirtualCam != camSwitcher.ThisVirtualCam)
            {
                SetParents(false);
            }
        }
        private void SetParents(bool active)
        {
            if (environmentParent == null)
            {
                Debug.LogError("EnvironmentParent of " + this.gameObject.name + " is unassigned!");
                Debug.Break();
                return;
            }
            environmentParent.gameObject.SetActive(active);
            if (spriteParent == null)
            {
                Debug.LogWarning("SpriteParent of " + this.gameObject.name + " is unassigned!");
            }
            else
            {
                spriteParent.gameObject.SetActive(active);
            }
        }
    }
}