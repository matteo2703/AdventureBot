using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataManager
{
    public void SaveGame(GenericGameData data);
    public void LoadGame(GenericGameData data);
}
