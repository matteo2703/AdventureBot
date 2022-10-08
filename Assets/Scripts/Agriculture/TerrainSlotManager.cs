using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSlotManager : MonoBehaviour//, IDataManager
{
    public List<TerrainSlot> slots;

    private void Awake()
    {
        slots = new(FindObjectsOfType<TerrainSlot>(true));
    }

    //public void LoadGame(GenericGameData data)
    //{
    //    if (data.plowdedTerrainSlot.Count != 0)
    //    {
    //        for (int i = 0; i < data.plowdedTerrainSlot.Count || i < slots.Count; i++)
    //        {
    //            slots[i].plowded = data.plowdedTerrainSlot[i];
    //            slots[i].planted = data.plantedTerrainSlot[i];
    //            slots[i].actualState = data.growingState[i];

    //            slots[i].SetState();
    //        }
    //    }
    //}

    //public void SaveGame(GenericGameData data)
    //{
    //    data.plowdedTerrainSlot.Clear();
    //    data.growingState.Clear();
    //    data.plantedTerrainSlot.Clear();

    //    foreach (var slot in slots)
    //    {
    //        data.plowdedTerrainSlot.Add(slot.plowded);
    //        data.plantedTerrainSlot.Add(slot.planted);
    //        data.growingState.Add(slot.actualState);
    //    }
    //}
}
