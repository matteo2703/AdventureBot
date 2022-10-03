using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlowButton : MonoBehaviour
{
    public TerrainSlot slot;

    public void Plow()
    {
        if(slot != null)
            slot.Plow();
    }
}
