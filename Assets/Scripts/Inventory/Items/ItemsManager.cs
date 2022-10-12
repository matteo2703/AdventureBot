using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour, IPlayerManager
{
    public static ItemsManager Instance;

    public List<InventoryItem> items;
    public Dictionary<InventoryItem, int> inventory;

    public List<InventoryItem> prefabGameItems;
    public List<bool> prefabGameItemsDiscovered;

    private void Awake()
    {
        Instance = this;
        InitItems();
        InitDiscovered();
    }
    public void InitItems()
    {
        inventory = new();
        items = new List<InventoryItem>(FindAllItems());
        for (int i = 0; i < items.Count; i++)
            items[i].id = i;
    }
    private void InitDiscovered()
    {
        foreach (var item in prefabGameItems)
            prefabGameItemsDiscovered.Add(false);
    }
    public InventoryItem[] FindAllItems()
    {
        InventoryItem[] worldItems = FindObjectsOfType<InventoryItem>(true);
        return worldItems;
    }
    public void SetState(int id, ItemStates state)
    {
        foreach(InventoryItem item in FindAllItems())
        {
            if (item.id == id)
                item.state = state;
        }
    }

    public void AddPlayerObject(InventoryItem newItem)
    {
        bool find = false;
        if (newItem.item.isStackable)
        {
            foreach (var obj in inventory)
            {
                if (obj.Key.item == newItem.item)
                {
                    inventory[obj.Key]++;
                    find = true;
                    break;
                }
            }
            if (!find)
                inventory.Add(newItem, 1);
        }
        else
            inventory.Add(newItem, 1);

        SetPrefabDiscover(newItem);
    }
    public bool FindItemInInventory(InventoryItem item)
    {
        foreach(var obj in inventory)
        {
            if (obj.Key.item == item.item)
                return true;
        }
        return false;
    }
    public void SubtractPlayerObject(InventoryItem subtractItem)
    {
        inventory[subtractItem]--;
        if (inventory[subtractItem] == 0)
            inventory.Remove(subtractItem);
    }
    public void SutractObjectByName(Item item)
    {
        foreach (var obj in inventory)
        {
            if (obj.Key.item == item)
            {
                SubtractPlayerObject(obj.Key);
                return;
            }
        }
    }

    public void SaveGame(GenericPlayerData data)
    {
        data.itemStates.Clear();
        foreach (InventoryItem item in items)
            data.itemStates.Add(item.state);

        data.inventory.Clear();
        foreach (var item in inventory)
            data.inventory.Add(new SerializableInventoryItems(FindItemIdPosition(item.Key), item.Value));

        data.discoveredItems.Clear();
        foreach (bool prefab in prefabGameItemsDiscovered)
            data.discoveredItems.Add(prefab);
    }

    public void LoadGame(GenericPlayerData data)
    {
        for (int i = 0; i < data.itemStates.Count; i++)
            items[i].SetState(data.itemStates[i]);

        foreach (var item in data.inventory)
        {
            InventoryItem newItem = prefabGameItems[item.itemReferenceId];
            inventory.Add(newItem, item.quantity);
        }

        for (int i = 0; i < data.discoveredItems.Count; i++)
            prefabGameItemsDiscovered[i] = data.discoveredItems[i];
    }
    public int FindItemIdPosition(InventoryItem item)
    {
        for(int i = 0; i < prefabGameItems.Count; i++)
        {
            if (prefabGameItems[i].item == item.item)
                return i;
        }
        return -1;
    }
    private void SetPrefabDiscover(InventoryItem item)
    {
        for(int i = 0; i < prefabGameItems.Count; i++)
        {
            if (prefabGameItems[i].item == item.item)
                prefabGameItemsDiscovered[i] = true;
        }
    }
}
