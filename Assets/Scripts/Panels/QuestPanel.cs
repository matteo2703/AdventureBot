using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPanel : Panel
{
    QuestListController questListcontroller;
    private void Awake()
    {
        questListcontroller = GetComponent<QuestListController>();
        Activate();
    }

    private void Update()
    {
        if (active)
            Time.timeScale = 0f;
    }
    public void OpenPanel()
    {
        active = true;
        Activate();
        questListcontroller.OpenQuestBook();
    }
    public void ClosePanel()
    {
        Time.timeScale = 1f;
        active = false;
        Deactivate();
    }
}
