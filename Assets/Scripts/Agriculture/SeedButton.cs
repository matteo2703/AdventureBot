using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedButton : MonoBehaviour
{
    public TerrainSlot slot;

    public void Plant()
    {
        if (slot != null)
            slot.Plant();
    }
}
