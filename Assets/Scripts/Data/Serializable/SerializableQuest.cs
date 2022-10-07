using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableQuest
{
    public int id;
    public bool inProgress;
    public bool isCompleted;
    public SerializableQuest(bool inProgress, bool isCompleted, int id)
    {
        this.inProgress = inProgress;
        this.isCompleted = isCompleted;
        this.id = id;
    }
}
