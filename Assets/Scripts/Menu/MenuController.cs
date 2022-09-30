using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    [SerializeField] List<GameObject> buttons;
    public int selectedButton;

    private Input input;
    public bool active;
    public object ActionManager { get; private set; }

    private void Awake()
    {
        input = new Input();
        active = true;
        InitializeInput();

        selectedButton = 0;
    }

    private void OnEnable() { input.General.Enable(); }
    private void OnDisable() { input.General.Disable(); }

    public void SetButtonSelection(int selection)
    {
        selectedButton = selection;
    }

    private void InitializeInput()
    {
        input.General.MoveThrow.started += ctx =>
        {
            if (active)
            {
                Vector2 val = ctx.ReadValue<Vector2>();
                if (val.x > 0 || val.y < 0)
                {
                    //move to next element
                    if (selectedButton < buttons.Count - 1)
                    {
                        if (buttons[selectedButton + 1].activeInHierarchy)
                            selectedButton++;
                        else if (selectedButton < buttons.Count - 2)
                            selectedButton += 2;
                    }
                }
                else if (val.x < 0 || val.y > 0)
                {
                    //move to prev element
                    if (selectedButton > 0)
                    {
                        if (buttons[selectedButton - 1].activeInHierarchy)
                            selectedButton--;
                        else if (selectedButton - 1 > 0)
                            selectedButton -= 2;
                    }
                }
            }
        };
    }

    private void Update()
    {
        if (active)
        {
            if (SystemInfo.deviceType != DeviceType.Handheld)
            {
                if (selectedButton != -1)
                    EventSystem.current.SetSelectedGameObject(buttons[selectedButton]);
                else
                    EventSystem.current.SetSelectedGameObject(null);
            }

            if (selectedButton > 0 && !buttons[selectedButton].activeInHierarchy)
                selectedButton--;
        }
        else
        {
            selectedButton = -1;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
