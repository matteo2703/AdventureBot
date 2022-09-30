using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    Input input;
    CanvasController controller;
    [SerializeField] GameObject craftingButton;
    public bool interactable;

    private void Awake()
    {
        input = new Input();
        input.General.Enable();
        controller = FindObjectOfType<CanvasController>(true);
        craftingButton.SetActive(false);

        input.General.Interact.started += i =>
        {
            if (interactable)
                controller.ChangeCrafting();
        };
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            craftingButton.SetActive(true);
            interactable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            craftingButton.SetActive(false);
            interactable = false;
        }
    }
}
