using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipes", menuName = "Creafting Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] public List<InventoryItem> itemsNeeded;
    [SerializeField] public List<InventoryItem> itemsCreated;

    public int id;
    public string title;
    [TextArea] public string description;
    public int exp;

    public bool IsCraftable()
    {
        List<InventoryItem> inventoryToList = new();

        //get separated inventory elements
        foreach (var item in ItemsManager.Instance.inventory)
        {
            for (int i = 0; i < item.Value; i++)
                inventoryToList.Add(item.Key);
        }

        //check if there is sufficient elements to craft
        for (int i = 0; i < itemsNeeded.Count; i++)
        {
            bool deleted = false;
            foreach (InventoryItem item in inventoryToList)
            {
                if (item.item == itemsNeeded[i].item)
                {
                    inventoryToList.Remove(item);
                    deleted = true;
                    break;
                }
            }
            if (!deleted)
                return false;
        }
        return true;
    }
    public void TryToCraft()
    {
        if (IsCraftable())
            Craft();
    }
    private void Craft()
    {
        foreach (InventoryItem item in itemsNeeded)
            ItemsManager.Instance.SutractObjectByName(item.item);

        foreach (InventoryItem item in itemsCreated)
            ItemsManager.Instance.AddPlayerObject(item);

        CraftingQuest quest = FindObjectOfType<CraftingQuest>();
        if (quest != null)
            foreach (InventoryItem item in itemsCreated)
                quest.ControlItem(item.item);
    }
}
