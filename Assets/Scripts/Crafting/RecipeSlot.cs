using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeSlot : MonoBehaviour, IPointerClickHandler
{
    public event Action<RecipeSlot> OnItemClick;

    [SerializeField] public GameObject selectionBar;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private GameObject craftableIcon;

    public Recipe recipe;

    public void SetData(Recipe recipe)
    {
        this.recipe = recipe;
        title.text = this.recipe.title;
        SetCraftableIcon();
    }

    public void SetCraftableIcon()
    {
        if (recipe.IsCraftable())
            craftableIcon.SetActive(true);
        else
            craftableIcon.SetActive(false);
    }
    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Left)
        {
            OnItemClick?.Invoke(this);
        }
    }
    public void Select(bool select)
    {
        selectionBar.SetActive(select);
    }
}