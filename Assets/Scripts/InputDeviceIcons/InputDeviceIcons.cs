using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;

[CreateAssetMenu(fileName = "InputDeviceIcons", menuName = "InputDeviceIcons")]
public class InputDeviceIcons : ScriptableObject
{
    public InputIconType[] InputIcons;
}
[System.Serializable]
public struct InputIconType
{
    public string Name;
    public Sprite Keyboard, Xbox, Playstation;
    public readonly Sprite GetControllerType(string thisScheme)
    {
        if (thisScheme.Equals("Gamepad"))
        {
            Debug.Log(thisScheme);
            var gamepad = Gamepad.current;
            if (gamepad is DualShockGamepad)
                return Playstation;
            else if (gamepad is XInputController)
            {
                Debug.Log("XBOX");
                return Xbox;
            }
            else
            {
                Debug.Log("NULL");
                return Keyboard;
            }
        }
        else
        {
            return Keyboard;
        }
    }
}