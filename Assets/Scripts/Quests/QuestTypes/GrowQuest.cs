using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowQuest : Quest, IDataManager

{
    public int numberOfGrowingPlantsNeeded;
    public int plantsGrowed;

    public void LoadGame(GenericGameData data)
    {
        if(inProgress)
            plantsGrowed = data.actualGrowQuestProgress;
    }

    public void SaveGame(GenericGameData data)
    {
        if (inProgress)
            data.actualGrowQuestProgress = plantsGrowed;
    }

    private void Update()
    {
        if (inProgress)
        {
            if (plantsGrowed == numberOfGrowingPlantsNeeded)
                EndQuest();
        }
    }
}
