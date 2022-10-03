using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestButton : MonoBehaviour
{
    public TerrainSlot slot;

    public void Harvest()
    {
        if(slot != null)
            slot.Harvest();
    }
}
