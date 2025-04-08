using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Ghoulish.PlayerControls;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerInput_Movement : MonoBehaviour
{
    [SerializeField] private bool _tankControls = false;
    private CharacterController _controller;
    public float _playerSpeed = 6f;
    [SerializeField] private float _gravityValue = -9f;
    [SerializeField] private float _controllerDeadzone = 0.1f;
    [SerializeField] private float _turnSmoothTime = 0.1f;
    [SerializeField] private float _movementSmoothSpeed = 5f;

    private ThirdPersonControls _thirdPersonControls;
    private PlayerInput _playerInput;
    private float _turnSmoothVelocity;
    private Vector2 _movement;
    private Vector3 _playerVelocity, _targetVelocity;

    

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _thirdPersonControls = new ThirdPersonControls();
        _playerInput = GetComponent<PlayerInput>();
        _playerVelocity = new Vector3(0f,0f,0f);
        _targetVelocity = new Vector3(0f,0f,0f);
    }

    private void OnEnable()
    {
        _thirdPersonControls.Enable();
    }

    private void OnDisable()
    {
        _thirdPersonControls.Disable();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        _movement = _thirdPersonControls.Controls.Movement.ReadValue<Vector2>();
    }

    void HandleMovement()
    {
        if (_tankControls)
        {
            float speed = _movement.y < 0 ? _playerSpeed * 0.25f : _playerSpeed; //Make player speed reduced if moving backwards
            _controller.Move(transform.forward * _movement.y * speed * Time.deltaTime);
            transform.Rotate(transform.up, _movement.x * _turnSmoothTime * Time.deltaTime);
        }
        else
        {
            
            Vector3 direction = new Vector3(_movement.x, 0, _movement.y);
            _targetVelocity = new Vector3(0f,0f,0f);
            if (GameManager.Instance._characterMovementEnabled)
            {
                if (direction.magnitude >= _controllerDeadzone)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                    _targetVelocity = direction.magnitude * transform.forward;
                }
            }
            _targetVelocity.y = _gravityValue;
            _playerVelocity = Vector3.MoveTowards(_playerVelocity, _targetVelocity, _movementSmoothSpeed * Time.fixedDeltaTime);
            _controller.Move(_playerSpeed * Time.fixedDeltaTime * _playerVelocity);
        }

    }
    public IEnumerator NudgePlayer(int duration, float speed)
    {
        GameManager.Instance._characterMovementEnabled = false;
        int i = 0;
        while (i < duration)
        {
            i++;
            _controller.Move(_controller.transform.forward * speed);
            yield return null;
        }
        GameManager.Instance._characterMovementEnabled = true;
    }
}
