using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }

    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    public List<IPlayerManager> dataObjects;
    public GenericPlayerData gameData;
    private FileDataHandler dataHandler;
    private string selectedProfileId = "";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        selectedProfileId = DataManager.Instance.selectedProfileId;
    }

    public void NewGame()
    {
        gameData = new GenericPlayerData();
    }
    public void LoadGame()
    {

        dataObjects = FindDataObjects();
        gameData = dataHandler.LoadPlayer(selectedProfileId);

        if (gameData == null)
            NewGame();

        foreach (IPlayerManager data in dataObjects)
        {
            data.LoadGame(gameData);
        }
    }
    public void SaveGame()
    {
        if (gameData == null)
            return;

        foreach (IPlayerManager data in dataObjects)
        {
            data.SaveGame(gameData);
        }

        dataHandler.SavePlayer(gameData, selectedProfileId);
    }
    public void BackToMenu()
    {
        Destroy(gameObject);
    }

    public List<IPlayerManager> FindDataObjects()
    {
        IEnumerable<IPlayerManager> dataObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IPlayerManager>();
        return new List<IPlayerManager>(dataObjects);
    }
    public bool HasGameData()
    {
        return gameData != null;
    }
    public Dictionary<string, GenericGameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
}
