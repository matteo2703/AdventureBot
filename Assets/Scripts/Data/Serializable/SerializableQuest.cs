using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableQuest
{
    public bool inProgress;
    public bool isCompleted;
    public SerializableQuest(bool inProgress, bool isCompleted)
    {
        this.inProgress = inProgress;
        this.isCompleted = isCompleted;
    }
}
