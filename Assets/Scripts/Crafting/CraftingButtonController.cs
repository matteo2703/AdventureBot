using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingButtonController : Panel
{
    [SerializeField] Button craftButton;
    RecipeController recipeController;

    public MenuController controller;
    public Recipe recipe;

    private void Awake()
    {
        controller = GetComponent<MenuController>();
        recipeController = FindObjectOfType<RecipeController>(true);
        ControllerDeactivation();
    }

    public void SetRecipe(Recipe recipe)
    {
        this.recipe = recipe;
        if (recipe.IsCraftable())
            active = true;
        else
            active = false;
        Activate();
    }
    public void CraftItem()
    {
        if(recipe!=null && active)
        {
            recipe.TryToCraft();
            recipeController.RefreshRecipeList();
            ControllerActivation();
        }
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
