using UnityEngine;
using UnityEngine.InputSystem;

namespace Ghoulish.PlayerControls
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController_ThirdPerson : IPlayerControls
    {
        [SerializeField] private float _turnSmoothTime = 0.1f;
        [SerializeField] private float _controllerDeadzone = 0.1f;
        private float _turnSmoothVelocity;
        private ThirdPersonControls _thirdPersonControls;
        private Vector3 _direction;
        protected override void Awake()
        {
            base.Awake();
            _thirdPersonControls = new ThirdPersonControls();
            _thirdPersonControls.Enable();
        }

        protected override void HandleInput()
        {
            Vector3 dir = _thirdPersonControls.Controls.Movement.ReadValue<Vector2>();
            _direction = new Vector3(dir.x, 0, dir.y).normalized;
            playerIsMoving = _direction.magnitude >= _controllerDeadzone;
        }

        protected override void HandleMovement()
        {
            float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = _direction.magnitude * transform.forward;
            movement += playerSpeed * Time.deltaTime * moveDir;
        }
    }
}