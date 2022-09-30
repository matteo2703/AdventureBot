using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NextQuest",menuName ="Quests/Listeners/Next Quest Listener")]
public class NextQuestListener : Listener
{
    public override void ActivateListener()
    {
        Quest nextQuest = QuestManager.Instance.GetQuest(QuestManager.Instance.lastQuestId + 1);
        if (nextQuest != null)
            nextQuest.AcceptQuest();
    }
}
