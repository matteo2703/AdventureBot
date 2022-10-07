using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUnloader : MonoBehaviour
{
    [SerializeField] string prevScene;
    [SerializeField] string thisScene;

    public IEnumerator UnloadPrevScene()
    {
        DataManager.Instance.SaveGame();
        yield return new WaitForSeconds(5f);
        SceneManager.UnloadSceneAsync(prevScene);

        GameData.Instance.thisScene = SceneManager.GetSceneByName(thisScene).buildIndex;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(UnloadPrevScene());
    }
}
