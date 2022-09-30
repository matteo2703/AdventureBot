using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingQuest : Quest
{
    [SerializeField] List<Item> itemsToCraft;
    [SerializeField] List<InventoryItem> itemToHide;
    public List<int> itemCrafted;

    bool createdAll;

    public override void Initialize()
    {
        itemCrafted = new();
        foreach (var item in itemsToCraft)
            itemCrafted.Add(0);

        foreach (var item in itemToHide)
            item.gameObject.SetActive(false);

        createdAll = false;
    }
    private void Update()
    {
        if (inProgress)
        {
            foreach (InventoryItem item in itemToHide)
                if (item.state == ItemStates.inWorld)
                    item.gameObject.SetActive(true);

            foreach (var item in itemCrafted)
            {
                createdAll = true;
                if (item != 1)
                {
                    createdAll = false;
                    break;
                }
            }

            if (createdAll)
                EndQuest();
        }
    }

    public void ControlItem(Item item)
    {
        for(int i = 0; i < itemsToCraft.Count; i++)
        {
            if (itemsToCraft[i] == item && itemCrafted[i] == 0)
                itemCrafted[i] = 1;
        }
    }
}
