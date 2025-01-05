using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using System;

// add this into built-in button you can create from menu: UI/Button
public class InputDeviceChangeHandler : MonoBehaviour {
    private PlayerInput _playerInput;

    // refs to Button's components
    private Image _buttonImage;
    private Sprite _thisIcon;

    [SerializeField] private InputDeviceIcons _inputDeviceIcons;

    public enum ButtonType {Cancel, Confirm, TabLeft, TabRight, OpenBackpack}
    public ButtonType buttonType;
    private string currentControlScheme;
 
    void Awake() {
        _playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        currentControlScheme = _playerInput.currentControlScheme;
        _buttonImage = GetComponent<Image>();
        _buttonImage.sprite = GetButtonIcon();
    }
    void Update()
    {
        if (_playerInput.currentControlScheme != currentControlScheme)
        {
            currentControlScheme = _playerInput.currentControlScheme;
            _buttonImage.sprite = GetButtonIcon();
        }
    }
 
    void OnEnable() {
        _playerInput.onControlsChanged += onChange;
    }
    void OnDisable() {
        _playerInput.onControlsChanged -= onChange;
    }
    void onChange(PlayerInput input) {
        _buttonImage.sprite = GetButtonIcon();
    }

    private Sprite GetButtonIcon()
    {
        Sprite outputSprite = null;
        for(int i =0; i < _inputDeviceIcons.InputIcons.Length;i++)
        {
            if (_inputDeviceIcons.InputIcons[i].Name == buttonType.ToString())
            {
                outputSprite = _inputDeviceIcons.InputIcons[i].GetControllerType(currentControlScheme);
            }
        }
        if (outputSprite != null)
        {
            return outputSprite;
        }
        else
        {
            Debug.LogError("Could not find sprite for input controller!");
            return null;
        }
    }
}