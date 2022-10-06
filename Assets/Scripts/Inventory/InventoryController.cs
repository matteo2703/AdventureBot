using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    private Input input;

    public List<Slot> slots;
    [SerializeField] Slot prefabSlot;
    [SerializeField] RectTransform slotContainer;
    [SerializeField] ItemDescriptionController descriptionController;
    [SerializeField] ItemButtonsController buttonsController;
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI levelText;

    private Vector2 movement;
    public int selectedItem = -1;
    public bool itemSelected;
    public bool intoObject;

    private void Awake()
    {
        input = new Input();
    }
    private void OnEnable() { input.General.Enable(); }
    private void OnDisable() { input.General.Disable(); }

    private void Start()
    {
        input.General.MoveThrow.started += MoveThrowItems;
        input.General.Confirm.started += IntoObjectAction;
        input.General.Back.started += OutsideObjectAction;
    }

    public void MoveThrowItems(InputAction.CallbackContext context)
    {
        if (!itemSelected)
        {
            movement = context.ReadValue<Vector2>();

            if (movement.x < 0 && selectedItem > 0)
                selectedItem--;
            else if (movement.x > 0 && selectedItem < slots.Count - 1)
                selectedItem++;
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

    public void Initialize(Dictionary<InventoryItem, int> objects)
    {
        intoObject = false;
        coinsText.text = PlayerStats.Instance.coins.ToString();
        timeText.text = $"{TimeController.Instance.hourOfDay:00}:{TimeController.Instance.minutesOfHour:00}";
        levelText.text = $"LIVELLO {PlayerStats.Instance.level}";
        descriptionController.ResetDescription();
        
        foreach(var item in objects)
        {
            Slot slot = Instantiate(prefabSlot, slotContainer);
            slot.SetData(item);
            slot.OnItemClick += ItemSelection;
            slots.Add(slot);
        }

        if (objects.Count > 0)
        {
            selectedItem = 0;
            ItemSelection(slots[selectedItem]);
        }
    }

    public void ItemSelection(Slot slot)
    {
        selectedItem = slots.IndexOf(slot);
        if (selectedItem == -1)
            return;

        SelectCorrectItem(selectedItem);
        descriptionController.SetDescription(slots[selectedItem].item.Key);
        buttonsController.SetItem(slots[selectedItem].item.Key);
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

    public void ClearSlots()
    {
        while (slots.Count > 0)
            DeleteSlot(0);
    }
    public void DeleteSlot(int index)
    {
        Destroy(slots[index].gameObject);
        slots.Remove(slots[index]);
    }

    public void UseItem()
    {
        bool useItem = buttonsController.UseItem();
        if (useItem)
            SubtractQuantity();
    }
    public void SubtractQuantity()
    {
        slots[selectedItem].UpdateQuantity();
        ItemsManager.Instance.SubtractPlayerObject(slots[selectedItem].item.Key);
        if (slots[selectedItem].quantity == 0)
        {
            slots[selectedItem].item.Key.SetState(ItemStates.destroyed);
            if(selectedItem == slots.Count - 1)
            {
                selectedItem--;
                DeleteSlot(selectedItem + 1);
            }
            else
                DeleteSlot(selectedItem);

            descriptionController.ResetDescription();
        }
    }

    public void DiscardItem()
    {
        SubtractQuantity();
    }

    private void Update()
    {
        if (slots.Count != 0)
            ItemSelection(slots[selectedItem]);
    }
    public void Close()
    {
        descriptionController.ResetDescription();
        ClearSlots();
    }
}
