using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableQuest
{
    public int id;
    public string title;
    public string description;

    public bool inProgress;
    public bool isCompleted;
    public SerializableQuest(bool inProgress, bool isCompleted, int id, string title, string description)
    {
        this.inProgress = inProgress;
        this.isCompleted = isCompleted;
        this.id = id;
        this.title = title;
        this.description = description;
    }
}
