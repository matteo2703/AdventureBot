using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int id;
    public string title;
    [TextArea] public string description;
    public float expReleased;

    public bool inProgress;
    public bool isCompleted;
    [SerializeField] protected Listener nextQuest;
    [SerializeField] GameObject showBeforeStartMission;
    [SerializeField] float timeShow;

    [SerializeField] public List<GameObject> hideObjects;

    private void Awake()
    {
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
            
            QuestManager.Instance.lastQuestId = id;
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
        CanvasController.Instance.gameObject.SetActive(false);
        showBeforeStartMission.SetActive(true);
        yield return new WaitForSeconds(timeShow);
        showBeforeStartMission.SetActive(false);
        CanvasController.Instance.gameObject.SetActive(true);
    }
    public void EndQuest()
    {
        inProgress = false;
        isCompleted = true;
        SetStatus();
        PlayerStats.Instance.MoreIntelligence();
        PlayerStats.Instance.AddExp(expReleased);

        if (nextQuest != null)
            nextQuest.ActivateListener();
    }
    public Quest FindNextQuest()
    {
        return FindSpecificQuest(id + 1);
    }
    public Quest FindSpecificQuest(int findId)
    {
        return QuestManager.Instance.GetQuest(findId);
    }
}
