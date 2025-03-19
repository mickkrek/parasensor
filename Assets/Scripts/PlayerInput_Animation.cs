using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Ghoulish.PlayerControls;

public class PlayerInput_Animation : MonoBehaviour
{
    private CharacterController _controller;
    private IPlayerControls _playerControls;
    [SerializeField] private Animator _animator;
    private float _forwardSpeed = 0f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerControls = GetComponent<IPlayerControls>();
    }

    private void Update()
    {
        UpdateLocomotion();
        //_animator.transform.position = transform.position;
        //_animator.transform.rotation = transform.rotation;
    }
    private void UpdateLocomotion()
    {
        if (_animator != null) 
        {
            Vector3 forwardVelocity = new Vector3(_controller.velocity.x, 0, _controller.velocity.z);
            Debug.Log(forwardVelocity.magnitude);
            // The speed on the x-z plane ignoring any speed
            _forwardSpeed = forwardVelocity.magnitude;
            // The speed from gravity or jumping
            float verticalSpeed = _controller.velocity.y;
            // The overall speed
            float overallSpeed = _controller.velocity.magnitude;
            
            _animator.SetFloat("ForwardVelocity",  _forwardSpeed);
        }
    }
}
