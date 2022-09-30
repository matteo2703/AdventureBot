using System.Collections.Generic;
using UnityEngine;

public class CollectQuest : Quest
{
    [SerializeField] List<InventoryItem> objectsToTake;

    public override void Initialize()
    {
        foreach (InventoryItem item in objectsToTake)
            item.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (inProgress)
        {
            foreach (InventoryItem item in objectsToTake)
                if (item.state == ItemStates.inWorld)
                    item.gameObject.SetActive(true);

            if (objectsToTake.Count == 0)
                EndQuest();

            for (int i = 0; i < objectsToTake.Count; i++)
                if (objectsToTake[i].state != ItemStates.inWorld)
                    objectsToTake.Remove(objectsToTake[i]);
        }
    }
}
