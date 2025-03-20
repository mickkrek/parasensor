using UnityEngine;
using Ghoulish.PlayerControls;
using Unity.Cinemachine;

namespace Ghoulish.FixedCameraSystem
{
    public class CameraSwitcher : MonoBehaviour
    {
        public CinemachineVirtualCameraBase ThisVirtualCam { get; private set; }
        [SerializeField] private float _pushForwardAmount = 1f;
        [SerializeField] private int _nudgeDuration = 1;
        private float _nudgeSpeed = 0.01f;
        private IPlayerControls _playerInput;

        private void Start()
        {
            _playerInput = CameraManager.Instance.PlayerInput;
            ThisVirtualCam = transform.parent.GetComponentInChildren<CinemachineVirtualCameraBase>();
            if (ThisVirtualCam != CameraManager.Instance.ActiveVirtualCam) 
            {
                ThisVirtualCam.Priority = 0;
                ThisVirtualCam.enabled = false;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (CameraManager.Instance.ActiveVirtualCam == ThisVirtualCam)
                {
                    return;
                }
                ThisVirtualCam.enabled = true;
                CameraManager.Instance.ActiveVirtualCam = ThisVirtualCam;
                CameraManager.Instance.UpdateActiveCamera();
                //Point the player toward the collision point and 'nudge' them forward for a second
                //Vector3 lookAtPos = GetComponent<Collider>().ClosestPoint(other.transform.position);
                //StartCoroutine(_playerInput.NudgePlayer(_nudgeDuration, _nudgeSpeed, lookAtPos, _pushForwardAmount, true)); //slowly nudge player forward
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ThisVirtualCam.enabled = false;
                //CheckForTriggers(); //To prevent camera target not getting set, search for new triggers when exiting. Edge cases
            }
        }

        void CheckForTriggers()
        {
            Collider[] colliderArray = Physics.OverlapSphere
            (
                _playerInput.transform.position, 
                _playerInput.GetComponent<CharacterController>().radius
            );
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out CameraSwitcher target) && (collider.gameObject != this.gameObject))
                {
                    CameraManager.Instance.ActiveVirtualCam = target.ThisVirtualCam;
                    ThisVirtualCam.enabled = true;
                    return;
                }
            }
        }
    }
}