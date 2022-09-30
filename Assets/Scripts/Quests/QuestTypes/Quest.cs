using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    QuestManager questManager;
    PlayerStats stats;

    public int id;
    public string title;
    [TextArea] public string description;
    public float expReleased;

    public bool inProgress;
    public bool isCompleted;
    [SerializeField] protected Listener nextQuestListener;
    [SerializeField] GameObject showBeforeStartMission;

    private void Awake()
    {
        questManager = FindObjectOfType<QuestManager>(true);
        stats = FindObjectOfType<PlayerStats>(true);
        SetStatus();
    }

    public virtual void Initialize() { }
    public void InitQuest(int id)
    {
        this.id = id;
        inProgress = false;
        isCompleted = false;
        Initialize();
        SetStatus();
    }
    public void SetStatus()
    {
        if(inProgress && !isCompleted)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

    public void AcceptQuest()
    {
        if(!inProgress && !isCompleted)
        {
            inProgress = true;
            SetStatus();
            questManager.lastQuestId = id;
            if (showBeforeStartMission != null)
            {
                StartCoroutine(ShowInfoQuest());
            }
        }
    }
    public IEnumerator ShowInfoQuest()
    {
        Time.timeScale = 0f;
        showBeforeStartMission.SetActive(true);
        yield return new WaitForSeconds(5f);
        showBeforeStartMission.SetActive(false);
        Time.timeScale = 1f;
    }
    public void EndQuest()
    {
        inProgress = false;
        isCompleted = true;
        SetStatus();
        stats.MoreIntelligence();
        stats.AddExp(expReleased);

        if (nextQuestListener != null)
            nextQuestListener.ActivateListener();
    }
    public Quest FindNextQuest()
    {
        return FindSpecificQuest(id + 1);
    }
    public Quest FindSpecificQuest(int findId)
    {
        return questManager.GetQuest(findId);
    }
}
