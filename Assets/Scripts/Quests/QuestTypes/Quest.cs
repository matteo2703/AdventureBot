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
    [SerializeField] float timeShow;

    CanvasController canvasController;

    [SerializeField] public List<GameObject> hideObjects;

    private void Awake()
    {
        questManager = FindObjectOfType<QuestManager>(true);
        stats = FindObjectOfType<PlayerStats>(true);
        canvasController = FindObjectOfType<CanvasController>(true);

        SetStatus();
        if(showBeforeStartMission != null)
            showBeforeStartMission.SetActive(false);
    }
    public void HideObjects()
    {
        foreach (var obj in hideObjects)
            obj.SetActive(false);
    }
    public void ShowObjects() 
    {
        foreach (var obj in hideObjects)
            obj.SetActive(false);
    }

    public virtual void Initialize() { }
    public void InitQuest(int id)
    {
        this.id = id;
        inProgress = false;
        isCompleted = false;
        Initialize();
        SetStatus();
        HideObjects();
    }
    public void SetStatus()
    {
        if (inProgress && !isCompleted)
        {
            gameObject.SetActive(true);
            ShowObjects();  
        }
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

            foreach (var obj in hideObjects)
                obj.SetActive(true);
        }
    }
    public IEnumerator ShowInfoQuest()
    {
        canvasController.gameObject.SetActive(false);
        showBeforeStartMission.SetActive(true);
        yield return new WaitForSeconds(timeShow);
        showBeforeStartMission.SetActive(false);
        canvasController.gameObject.SetActive(true);
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
