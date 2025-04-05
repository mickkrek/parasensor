using UnityEngine;

namespace Ghoulish.FixedCameraSystem
{
    public class ZoneGraphicsToggle : MonoBehaviour
    {
        private CameraSwitcher camSwitcher;
        [SerializeField] private Transform spriteParent;
        [SerializeField] private Transform environmentParent;

        void OnEnable()
        {
            camSwitcher = GetComponentInChildren<CameraSwitcher>();
        }

        void Update()
        {
            if (CameraManager.Instance.ActiveVirtualCam == camSwitcher.ThisVirtualCam)
            {
                if (spriteParent != null && !spriteParent.gameObject.activeSelf)
                {
                    spriteParent.gameObject.SetActive(true);
                }
                if (environmentParent != null && !environmentParent.gameObject.activeSelf)
                {
                    environmentParent.gameObject.SetActive(true);
                }
            }
            else if (CameraManager.Instance.ActiveVirtualCam != camSwitcher.ThisVirtualCam)
            {
                if (spriteParent != null && spriteParent.gameObject.activeSelf)
                {
                    spriteParent.gameObject.SetActive(false);
                }
                if (environmentParent != null && environmentParent.gameObject.activeSelf)
                {
                    environmentParent.gameObject.SetActive(false);
                }
            }
        }
    }
}