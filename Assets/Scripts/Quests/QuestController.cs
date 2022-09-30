using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestController : MonoBehaviour
{
    [SerializeField] QuestDescriptionController controller;

    [SerializeField] QuestSlot prefabSlot;
    [SerializeField] RectTransform slotContainer;

    List<QuestSlot> slots = new();

    public Action<int> OnDescriptionRequest;
    public int selectedItem = -1;
    Vector2 movement;
    private Input input;

    private void Awake()
    {
        input = new Input();
    }
    private void OnEnable() { input.General.Enable(); }
    private void OnDisable() { input.General.Disable(); }
    private void Start()
    {
        input.General.MoveThrow.started += MoveThrowItems;
    }
    public void MoveThrowItems(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();

        if (movement.y > 0 && selectedItem > 0)
            selectedItem -= 1;
        else if (movement.y < 0 && selectedItem < slots.Count - 1)
            selectedItem += 1;
    }
    public void Initialize(List<Quest> quests)
    {
        controller.ResetDescription();
        DeselectAll();
        ClearSlots();
        foreach (var item in quests)
        {
            QuestSlot slot = Instantiate(prefabSlot, slotContainer);

            slot.SetData(item);
            slot.OnItemClick += ItemSelection;

            slots.Add(slot);
        }

        if (slots.Count > 0)
        {
            selectedItem = QuestManager.Instance.lastQuestRead == -1 ? 0 : QuestManager.Instance.lastQuestRead;
            ItemSelection(slots[selectedItem]);
        }
    }

    public void ClearSlots()
    {
        while (slots.Count > 0)
        {
            DeleteSlot(0);
        }
    }

    public void DeleteSlot(int slot)
    {
        Destroy(slots[slot].gameObject);
        slots.Remove(slots[slot]);
    }

    public void ItemSelection(QuestSlot slot)
    {
        selectedItem = slots.IndexOf(slot);
        if (selectedItem == -1)
            return;

        SetDescription(slots[selectedItem].quest);
        slots[selectedItem].DeselectNewQuestIcon();
        DeselectAll();
        slots[selectedItem].selectionBar.SetActive(true);
        QuestManager.Instance.lastQuestRead = slots[selectedItem].quest.id > QuestManager.Instance.lastQuestRead ? slots[selectedItem].quest.id : QuestManager.Instance.lastQuestRead;
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
    }

    public void SetDescription(Quest quest)
    {
        controller.SetDescription(quest);
    }
    private void Update()
    {
        if (slots.Count != 0)
            ItemSelection(slots[selectedItem]);
    }
}
