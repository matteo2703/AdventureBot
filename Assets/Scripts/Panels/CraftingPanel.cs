using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingPanel : Panel
{
    RecipeListController recipeController;
    private void Awake()
    {
        recipeController = GetComponent<RecipeListController>();
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
        Activate();
        recipeController.OpenRecipeBook();
    }
    public void ClosePanel()
    {
        Time.timeScale = 1f;
        active = false;
        Deactivate();
    }
}
