using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeDescriptionController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;

    public void ActivePanel()
    {
        gameObject.SetActive(true);
    }

    public void DeactivePanel()
    {
        gameObject.SetActive(false);
    }

    public void SetDescription(Recipe recipe)
    {
        ActivePanel();

        title.text = recipe.title;
        description.text = recipe.description;
    }

    public void ResetDescription()
    {
        title.text = "";
        description.text = "";

        DeactivePanel();
    }
}
