using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : Panel
{
    [SerializeField] List<Panel> panels;

    private void Awake()
    {
        active = false;
    }

    public void SetPause()
    {
        DataManager.Instance.dataObjects =  DataManager.Instance.FindDataObjects();
        PlayerDataManager.Instance.dataObjects = PlayerDataManager.Instance.FindDataObjects();
        Time.timeScale = 0f;
        foreach (Panel panel in panels)
            panel.Deactivate();

        gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        foreach (Panel panel in panels)
            panel.Activate();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        DataManager.Instance.BackToMenu();
        SceneManager.LoadSceneAsync("MainMenu");

        DontDestroy[] objectsToDestroy = FindObjectsOfType<DontDestroy>();
        foreach (DontDestroy obj in objectsToDestroy)
            obj.Destroy();
    }

    public void Save()
    {
        DataManager.Instance.SaveGame();
        PlayerDataManager.Instance.SaveGame();
    }
}
