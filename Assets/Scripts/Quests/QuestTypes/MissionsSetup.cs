using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsSetup : MonoBehaviour
{
    public Quest[] quests;
    private void Awake()
    {
        quests = FindObjectsOfType<Quest>(true);
        foreach (Quest quest in quests)
            quest.Initialize();
    }
}
