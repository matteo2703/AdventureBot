using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour, IDataManager
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void LoadGame(GenericGameData data)
    {
        transform.position = data.npc.position;
        transform.rotation = data.npc.rotation;
    }

    public void SaveGame(GenericGameData data)
    {
        Debug.Log("");
    }
}
