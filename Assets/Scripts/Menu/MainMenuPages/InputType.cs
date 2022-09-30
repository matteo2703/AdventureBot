using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class InputType : MonoBehaviour
{
    [SerializeField] GameObject gamepadScheme;
    [SerializeField] GameObject keyboardScheme;
    [SerializeField] GameObject touchScheme;
    private void OnEnable()
    {
        DeactivateSchemes();
        if (SystemInfo.deviceType == DeviceType.Handheld)
            touchScheme.SetActive(true);
        else if (Joystick.current != null || Gamepad.current != null)
            gamepadScheme.SetActive(true);
        else
            keyboardScheme.SetActive(true);
    }

    private void DeactivateSchemes()
    {
        gamepadScheme.SetActive(false);
        keyboardScheme.SetActive(false);
        touchScheme.SetActive(false);
    }
}
