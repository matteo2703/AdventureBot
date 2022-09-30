using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListController : MonoBehaviour
{
    [SerializeField] QuestController slotsController;
    [SerializeField] List<Quest> quests = new();

    private void Awake()
    {
        slotsController = FindObjectOfType<QuestController>(true);
    }
    public void OpenQuestBook()
    {
        quests.Clear();
        quests = QuestManager.Instance.GetQuests();
        gameObject.SetActive(true);
        slotsController.Initialize(quests);
    }

    public void CloseQuestBook()
    {
        gameObject.SetActive(false);
        slotsController.Close();
    }
}