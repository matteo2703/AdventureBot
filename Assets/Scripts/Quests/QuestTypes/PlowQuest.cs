using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlowQuest : Quest
{
    [SerializeField] List<TerrainSlot> slotsToPlow;

    private void Update()
    {
        if (inProgress)
        {
            bool allPlowded = false;
            foreach (var slot in slotsToPlow)
            {
                if (slot.plowded)
                    allPlowded = true;
                else
                {
                    allPlowded = false;
                    break;
                }
            }
            if (allPlowded)
                EndQuest();
        }
    }
}
