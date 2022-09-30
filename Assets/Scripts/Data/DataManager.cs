using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private List<IDataManager> dataObjects;
    public GenericGameData gameData;
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
        DontDestroyOnLoad(gameObject);

        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dataObjects = FindDataObjects();
        LoadGame();
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        selectedProfileId = newProfileId;
        LoadGame();
    }

    public void DeleteProfileData(string profileId)
    {
        dataHandler.Delete(profileId);
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GenericGameData();
    }
    public void LoadGame()
    {
        gameData = dataHandler.Load(selectedProfileId);

        if (gameData == null)
            NewGame();

        foreach (IDataManager data in dataObjects)
        {
            data.LoadGame(gameData);
        }
    }
    public void SaveGame()
    {
        if (gameData == null)
            return;

        foreach (IDataManager data in dataObjects)
        {
            data.SaveGame(gameData);
        }

        dataHandler.Save(gameData, selectedProfileId);
    }
    public void BackToMenu()
    {
        Destroy(gameObject);
    }

    private List<IDataManager> FindDataObjects()
    {
        IEnumerable<IDataManager> dataObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataManager>();
        return new List<IDataManager>(dataObjects);
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
