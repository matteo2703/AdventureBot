using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour, IDataManager
{
    PlayerStats playerStats;
    public int lastHourCheck;
    int thisScene;

    TimeController timeController;

    [SerializeField] InventoryItem item;
    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>(true);
        thisScene = SceneManager.GetActiveScene().buildIndex;
        timeController = FindObjectOfType<TimeController>(true);
        Cursor.visible = false;

        if (item != null)
            ItemsManager.Instance.AddPlayerObject(item);
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
