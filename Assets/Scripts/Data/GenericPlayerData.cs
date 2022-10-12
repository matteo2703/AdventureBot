using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GenericPlayerData
{
    public SerializableCamera camera;
    public SerializableObjectPosition player;
    public SerializablePlayerStat playerStat;

    public List<SerializableQuest> quests;
    public int lastQuestId;
    public int lastQuestRead;

    public List<ItemStates> itemStates;
    public List<SerializableInventoryItems> inventory;
    public List<bool> discoveredItems;
    public List<bool> discoverdRecipes;

    public GenericPlayerData()
    {
        camera = new SerializableCamera(new SerializableObjectPosition(new Vector3(9f, 0f, -12f), Quaternion.identity), 0f, 0f);
        player = new SerializableObjectPosition(new Vector3(9f, 0f, -12f), Quaternion.identity);
        playerStat = new SerializablePlayerStat(100, 100, 100, 0, 0, 0, 5, 0, 0, 1);

        quests = new();
        lastQuestId = -1;
        lastQuestRead = -1;

        itemStates = new();
        inventory = new();
        discoveredItems = new();
        discoverdRecipes = new();
    }
}
