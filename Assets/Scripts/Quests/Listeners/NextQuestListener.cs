using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NextQuest",menuName ="Quests/Listeners/Next Quest Listener")]
public class NextQuestListener : Listener
{
    public override void ActivateListener(int thisQuestId)
    {
        Quest[] nextQuests = FindObjectsOfType<Quest>(true);
        Quest nextQuest = null;
        foreach (Quest quest in nextQuests) {
            if (quest.id == thisQuestId + 1)
            {
                nextQuest = quest;
                break;
            }
        }
        if (nextQuest != null)
            nextQuest.AcceptQuest();
    }
}
