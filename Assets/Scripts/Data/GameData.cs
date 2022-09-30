using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour, IDataManager
{
    PlayerStats playerStats;
    public int lastHourCheck;
    int thisScene;

    TimeController timeController;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>(true);
        thisScene = SceneManager.GetActiveScene().buildIndex;
        timeController = FindObjectOfType<TimeController>(true);
        Cursor.visible = false;
    }
    public void LoadGame(GenericGameData data)
    {
        thisScene = data.lastScene;
    }

    public void SaveGame(GenericGameData data)
    {
        data.lastScene = thisScene;
    }

    private void Update()
    {
        Rusting();
    }
    private void Rusting()
    {
        if (timeController.hourOfDay != lastHourCheck)
        {
            lastHourCheck = timeController.hourOfDay;
            playerStats.Rusting();
        }
    }
}
