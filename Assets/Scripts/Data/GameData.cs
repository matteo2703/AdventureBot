using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour, IDataManager
{
    public static GameData Instance;

    public int lastHourCheck;
    public int thisScene;

    [SerializeField] InventoryItem item;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        thisScene = SceneManager.GetActiveScene().buildIndex;
        Cursor.visible = false;

        if (item != null)
            ItemsManager.Instance.AddPlayerObject(item);

        StartCoroutine(LoadAsyncScene("Tutorial2"));
    }
    
    public IEnumerator LoadAsyncScene(string scene)
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
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
        if (TimeController.Instance.hourOfDay != lastHourCheck)
        {
            lastHourCheck = TimeController.Instance.hourOfDay;
            PlayerStats.Instance.Rusting();
        }
    }
}
