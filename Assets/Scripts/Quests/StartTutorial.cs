using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTutorial : MonoBehaviour
{
    public int startTutorialQuestId;
    void Start()
    {
        Quest[] quests = QuestManager.Instance.WorldQuests();
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
