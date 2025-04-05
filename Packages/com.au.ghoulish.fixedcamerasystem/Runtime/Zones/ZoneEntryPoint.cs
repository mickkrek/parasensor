using Unity.Cinemachine;
using UnityEngine;

namespace Ghoulish.FixedCameraSystem
{
    public class ZoneEntryPoint : MonoBehaviour
    {
        [HideInInspector] public string ZoneName;
        public bool Active;
        [HideInInspector] public Collider ZoneCollider;
        private CinemachineVirtualCameraBase _thisVirtualCam;
        private CinemachineVirtualCameraBase _storedVirtualCam;


        private void Start()
        {
            ZoneName = transform.parent.name;
            ZoneCollider = GetComponent<Collider>();
            _storedVirtualCam = CameraManager.Instance.ActiveVirtualCam;
            _thisVirtualCam = transform.parent.GetComponentInChildren<CinemachineVirtualCameraBase>();
            if (_thisVirtualCam == _storedVirtualCam) 
            {
                Active = true;
            }
        }

        private void Update()
        {
            if (_storedVirtualCam != CameraManager.Instance.ActiveVirtualCam)//if active cam has changed
            {
                if (_thisVirtualCam == CameraManager.Instance.ActiveVirtualCam)  //if the new active cam is the same as this object's cam
                {
                    Active = true;
                } 
                else 
                {
                    Active = false;
                }
                _storedVirtualCam = CameraManager.Instance.ActiveVirtualCam;
            }
        }
    }
}