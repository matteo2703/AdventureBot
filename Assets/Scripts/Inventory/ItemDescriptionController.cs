using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionController : Panel
{
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;
    ItemButtonsController buttonController;

    private void Awake()
    {
        buttonController = FindObjectOfType<ItemButtonsController>(true);
    }

    public void SetDescription(InventoryItem item)
    {
        active = true;
        Activate();

        itemIcon.sprite = item.item.itemIcon;
        title.text = item.item.itemName;
        description.text = item.item.itemDescription;

        buttonController.SetItem(item);
    }
    public void ResetDescription()
    {
        itemIcon.sprite = null;
        title.text = "";
        description.text = "";

        buttonController.ControllerDeactivation();
        active = false;
        Deactivate();
    }
}
