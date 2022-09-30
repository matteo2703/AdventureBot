using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestSlot : MonoBehaviour, IPointerClickHandler
{
    public event Action<QuestSlot> OnItemClick;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private GameObject completeIcon;
    [SerializeField] private GameObject newQuestIcon;
    [SerializeField] public GameObject selectionBar;

    public Quest quest;

    public void SetData(Quest questItem)
    {
        quest = questItem;
        title.text = quest.title;

        if (quest.inProgress && QuestManager.Instance.lastQuestRead < quest.id)
            newQuestIcon.SetActive(true);
        else
            newQuestIcon.SetActive(false);

        if (quest.isCompleted)
        {
            completeIcon.SetActive(true);
            newQuestIcon.SetActive(false);
        }
        else
            completeIcon.SetActive(false);
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Left)
        {
            OnItemClick?.Invoke(this);
            DeselectNewQuestIcon();
        }
    }

    public void DeselectNewQuestIcon()
    {
        newQuestIcon.SetActive(false);
    }
}