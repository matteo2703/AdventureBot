using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour, IPlayerManager
{
    public List<Recipe> recipes;
    public List<bool> discoveredRecipes = new();
    private bool discovered;

    private void Awake()
    {
        SetDiscovered();
    }
    private void SetDiscovered()
    {
        discoveredRecipes = new();
        for (int i = 0; i < recipes.Count; i++)
            discoveredRecipes.Add(false);
    }
    public List<Recipe> GetReceips()
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (discoveredRecipes[i] == false)
            {
                foreach (InventoryItem item in recipes[i].itemsNeeded)
                {
                    if (ItemsManager.Instance.prefabGameItemsDiscovered[ItemsManager.Instance.FindItemIdPosition(item)] == true)
                        discovered = true;
                    else
                    {
                        discovered = false;
                        break;
                    }
                }
                discoveredRecipes[i] = discovered;
            }
        }

        List<Recipe> discover = new();
        for(int i=0; i < recipes.Count; i++)
        {
            if (discoveredRecipes[i] == true)
                discover.Add(recipes[i]);
        }

        return discover;
    }

    public void LoadGame(GenericPlayerData data)
    {
        for (int i = 0; i < data.discoverdRecipes.Count; i++)
            discoveredRecipes.Add(data.discoverdRecipes[i]);
    }

    public void SaveGame(GenericPlayerData data)
    {
        data.discoverdRecipes.Clear();
        foreach (bool recipe in discoveredRecipes)
            data.discoverdRecipes.Add(recipe);
    }
}
