using UnityEngine;
using UnityEngine.InputSystem;

namespace Ghoulish.PlayerControls
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController_ThirdPerson : IPlayerControls
    {
        [SerializeField] private float _turnSmoothTime = 0.1f;
        private float _turnSmoothVelocity;
        private ThirdPersonControls _thirdPersonControls;
        private Vector3 _direction, _velocity;
        protected override void Awake()
        {
            base.Awake();
            _thirdPersonControls = new ThirdPersonControls();
            _thirdPersonControls.Enable();
            _velocity = new Vector3(0f,0f,0f);
        }

        protected override void HandleInput()
        {
            Vector3 dir = _thirdPersonControls.Controls.Movement.ReadValue<Vector2>();
            _direction = new Vector3(dir.x, 0, dir.y);
            playerIsMoving = _direction.magnitude >= controllerDeadzone;
        }

        protected override void HandleMovement()
        {
            if (playerIsMoving)//only rotate transform when stick is moving
            {
                float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f); 
            }
            Vector3 targetDir = _direction.magnitude * transform.forward;
            _velocity = Vector3.MoveTowards(_velocity, targetDir, movementSmoothSpeed * Time.fixedDeltaTime);
            movement = playerSpeed * Time.fixedDeltaTime * _velocity;
        }
    }
}