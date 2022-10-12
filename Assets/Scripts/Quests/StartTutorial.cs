using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTutorial : MonoBehaviour
{
    public int startTutorialQuestId;
    public Quest[] quests;

    MissionsSetup missionSetup;

    private void Awake()
    {
        missionSetup = FindObjectOfType<MissionsSetup>();
    }
    void Start()
    {
        quests = missionSetup.quests;
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
