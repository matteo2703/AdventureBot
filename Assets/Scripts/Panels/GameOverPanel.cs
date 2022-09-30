using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : Panel
{
    private void Awake()
    {
        active = false;
        Activate();
    }
    public void ShowPanel()
    {
        active = true;
        Activate();
    }
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        DataManager.Instance.BackToMenu();
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
