using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonsController : Panel
{
    [SerializeField] Button useButton;
    [SerializeField] Button discardButton;

    public MenuController controller;
    public InventoryItem item;

    private void Awake()
    {
        controller = GetComponent<MenuController>();
        ControllerDeactivation();
    }

    public void SetItem(InventoryItem item)
    {
        this.item = item;

        if (item.item.itemType == ItemTypes.usable)
            active = true;
        else
            active = false;
        
        Activate();
    }
    public bool UseItem()
    {
        if (item != null && active)
        {
            item.TryToUse();
            return true;
        }
        return false;
    }

    public void ControllerActivation()
    {
        controller.SetButtonSelection(0);
        controller.active = true;
    }
    public void ControllerDeactivation()
    {
        controller.SetButtonSelection(-1);
        controller.active = false;
    }
}
