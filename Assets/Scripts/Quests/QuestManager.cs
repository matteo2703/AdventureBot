using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour, IPlayerManager
{
    public static QuestManager Instance;

    public List<Quest> questList;

    public int lastQuestId;
    public int lastQuestRead;
    public int questCounter;

    [SerializeField] GameObject newQuestAvailable;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        lastQuestRead = -1;
        lastQuestId = -1;

        questCounter = -1;
        foreach (Quest quest in questList)
            InitQuest(quest);
    }

    private void Update()
    {
        if (lastQuestRead != lastQuestId)
            newQuestAvailable.SetActive(true);
        else
            newQuestAvailable.SetActive(false);
    }

    public List<Quest> GetQuests()
    {
        List<Quest> quests = new();
        foreach(Quest quest in questList)
            if(quest.inProgress || quest.isCompleted)
                quests.Add(quest);

        return quests;
    }
    public Quest[] WorldQuests()
    {
        return FindObjectsOfType<Quest>(true);
    }
    public Quest GetQuest(int id)
    {
        foreach(Quest quest in questList)
        {
            if (quest.id == id)
                return quest;
        }

        return null;
    }
    public void AddQuest(Quest newQuest)
    {
        InitQuest(newQuest);
        questList.Add(newQuest);
    }
    public void InitQuest(Quest newQuest)
    {
        questCounter++;
        newQuest.InitQuest(questCounter);
    }

    public void SaveGame(GenericPlayerData data)
    {
        data.lastQuestId = lastQuestId;
        data.lastQuestRead = lastQuestRead;

        data.quests.Clear();
        foreach (Quest quest in questList)
        {
            data.quests.Add(new SerializableQuest(quest.inProgress, quest.isCompleted, quest.id, quest.title, quest.description));
        }
    }

    public void LoadGame(GenericPlayerData data)
    {
        lastQuestId = data.lastQuestId;
        lastQuestRead = data.lastQuestRead;

        for (int i = 0; i < data.quests.Count; i++)
        {
            Quest quest = Quest.FindSpecificQuest(data.quests[i].id);
            questList.Add(quest);
            questList[i].inProgress = data.quests[i].inProgress;
            questList[i].isCompleted = data.quests[i].isCompleted;
            questList[i].SetStatus();
        }
    }
}
