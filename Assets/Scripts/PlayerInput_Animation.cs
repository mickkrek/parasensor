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
    protected AnimatorOverrideController animatorOverrideController;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerControls = GetComponent<IPlayerControls>();
        animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = animatorOverrideController;
    }

    private void LateUpdate()
    {
        UpdateLocomotion();
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
    public void ChangeEquippedItemPose(AnimationClip newAnimation)
    {
        animatorOverrideController["EquippedItemPose"] = newAnimation;
        _animator.SetLayerWeight(1,1.0f);
    }
    public void RemoveEquippedItemPose()
    {
        animatorOverrideController["EquippedItemPose"] = null;
        _animator.SetLayerWeight(1,0.0f);
    }
}
