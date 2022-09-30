using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : Panel
{
    [SerializeField] InventoryController inventoryController;
    private void Awake()
    {
        Activate();
    }

    private void Update()
    {
        if (active)
            Time.timeScale = 0f;
    }
    public void OpenPanel()
    {
        active = true;
        gameObject.SetActive(true);
        inventoryController.Initialize(ItemsManager.Instance.inventory);
    }
    public void ClosePanel()
    {
        Time.timeScale = 1f;
        active = false;
        inventoryController.Close();
        gameObject.SetActive(false);
    }
}
