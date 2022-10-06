using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeListController : MonoBehaviour
{
    public static RecipeListController Instance;

    [SerializeField] RecipeController recipesController;
    [SerializeField] List<Recipe> craftingRecipes;

    RecipeManager receipeManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        recipesController = FindObjectOfType<RecipeController>(true);
        receipeManager = FindObjectOfType<RecipeManager>(true);
    }

    public void OpenRecipeBook()
    {
        craftingRecipes.Clear();
        craftingRecipes = receipeManager.GetReceips();
        gameObject.SetActive(true);
        recipesController.Initialize(craftingRecipes);
    }
    public void CloseRecipeBook()
    {
        gameObject.SetActive(false);
        recipesController.Close();
    }
}
