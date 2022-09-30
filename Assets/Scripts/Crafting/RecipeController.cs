using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RecipeController : MonoBehaviour
{
    [SerializeField] RecipeDescriptionController controller;

    [SerializeField] RecipeSlot prefabSlot;
    [SerializeField] RectTransform slotContainer;
    [SerializeField] CraftingButtonController buttonsController;

    RecipeManager manager;

    public int selectedItem = -1;
    public bool itemSelected;
    public bool intoObject;
    Vector2 movement;
    private Input input;

    List<RecipeSlot> slots = new();
    private void Awake()
    {
        input = new Input();
        manager = FindObjectOfType<RecipeManager>(true);
    }
    private void OnEnable() { input.General.Enable(); }
    private void OnDisable() 
    { 
        input.General.Disable();
        intoObject = false;
        itemSelected = false;
        buttonsController.ControllerDeactivation();
    }
    private void Start()
    {
        input.General.MoveThrow.started += MoveThrowItems;
        input.General.Confirm.started += IntoObjectAction;
        input.General.Back.started += OutsideObjectAction;

        buttonsController.ControllerDeactivation();
    }
    public void MoveThrowItems(InputAction.CallbackContext context)
    {
        if (!itemSelected)
        {
            movement = context.ReadValue<Vector2>();

            if (movement.y > 0 && selectedItem > 0)
                selectedItem -= 1;
            else if (movement.y < 0 && selectedItem < slots.Count - 1)
                selectedItem += 1;
        }
    }
    public void IntoObjectAction(InputAction.CallbackContext context)
    {
        if (buttonsController.isActiveAndEnabled && !intoObject)
        {
            intoObject = true;
            itemSelected = true;
            buttonsController.ControllerActivation();
        }
    }
    public void OutsideObjectAction(InputAction.CallbackContext contet)
    {
        if (intoObject)
        {
            intoObject = false;
            itemSelected = false;
            buttonsController.ControllerDeactivation();
        }
    }
    public void Initialize(List<Recipe> recipes)
    {
        controller.ResetDescription();
        DeselectAll();
        ClearSlots();
        foreach(var item in recipes)
        {
            RecipeSlot slot = Instantiate(prefabSlot, slotContainer);

            slot.SetData(item);
            slot.OnItemClick += ItemSelection;

            slots.Add(slot);
        }

        if (slots.Count > 0)
            ItemSelection(slots[0]);
    }
    public void RefreshRecipeList()
    {
        ClearSlots();
        foreach (var item in manager.GetReceips())
        {
            RecipeSlot slot = Instantiate(prefabSlot, slotContainer);

            slot.SetData(item);
            slot.OnItemClick += ItemSelection;

            slots.Add(slot);
        }
    }
    public void ItemSelection(RecipeSlot slot)
    {
        selectedItem = slots.IndexOf(slot);
        if (selectedItem == -1)
            return;

        buttonsController.SetRecipe(slots[selectedItem].recipe);
        DeselectAll();
        SelectCorrectItem(selectedItem);
        SetDescription(slots[selectedItem].recipe);
    }
    public void ResetCraftableIcons()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].SetCraftableIcon();
        }
    }
    public void SelectCorrectItem(int index)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i == index)
                slots[i].Select(true);
            else
                slots[i].Select(false);
        }
    }
    public void SetDescription(Recipe recipe)
    {
        controller.SetDescription(recipe);
    }
    public void DeselectAll()
    {
        foreach (var item in slots)
            item.selectionBar.SetActive(false);
    }
    public void Close()
    {
        controller.ResetDescription();
        ClearSlots();
        buttonsController.ControllerDeactivation();
    }
    public void ClearSlots()
    {
        while (slots.Count > 0)
            DeleteSlot(0);
    }
    public void DeleteSlot(int slot)
    {
        Destroy(slots[slot].gameObject);
        slots.Remove(slots[slot]);
    }

    private void Update()
    {
        if (slots.Count != 0)
            ItemSelection(slots[selectedItem]);
    }
}
