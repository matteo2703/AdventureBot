using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTutorial : MonoBehaviour
{
    QuestManager questManager;
    public int startTutorialQuestId;
    private void Awake()
    {
        questManager = FindObjectOfType<QuestManager>(true);
    }
    void Start()
    {
        Quest[] quests = questManager.WorldQuests();
        foreach (Quest quest in quests)
        {
            if (quest.id == startTutorialQuestId)
            {
                quest.AcceptQuest();
                return;
            }
        }
    }
}
