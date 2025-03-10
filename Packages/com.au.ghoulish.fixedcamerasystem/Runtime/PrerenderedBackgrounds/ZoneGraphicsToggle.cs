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
            camSwitcher = GetComponent<CameraSwitcher>();
        }

        void Update()
        {
            if (CameraManager.Instance.ActiveVirtualCam == camSwitcher.ThisVirtualCam)
            {
                if (spriteParent != null && !spriteParent.gameObject.activeSelf)
                {
                    spriteParent.gameObject.SetActive(true);
                }
                environmentParent.gameObject.SetActive(true);
            }
            else if (CameraManager.Instance.ActiveVirtualCam != camSwitcher.ThisVirtualCam)
            {
                if (spriteParent != null && spriteParent.gameObject.activeSelf)
                {
                    spriteParent.gameObject.SetActive(false);
                }
                environmentParent.gameObject.SetActive(false);
            }
        }
    }
}