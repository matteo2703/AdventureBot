using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileMenuManager : MonoBehaviour
{
    private ProfileSlot[] saveSlot;

    private void Awake()
    {
        saveSlot = GetComponentsInChildren<ProfileSlot>();
    }

    private void Start()
    {
        ActivateMenu();
    }

    public void OnClearClicked(ProfileSlot slot)
    {
        DataManager.Instance.DeleteProfileData(slot.GetProfileId());
        ActivateMenu();
    }

    public void ActivateMenu()
    {
        //load all existing profiles
        Dictionary<string, GenericGameData> profilesGameData = DataManager.Instance.GetAllProfilesGameData();

        //set the content based on the profile loaded
        foreach(ProfileSlot slot in saveSlot)
        {
            profilesGameData.TryGetValue(slot.GetProfileId(), out GenericGameData profileData);
            slot.SetData(profileData);
        }
    }
    public void OnSaveSlotCliked(ProfileSlot slot)
    {
        //update the selected profile id
        DataManager.Instance.ChangeSelectedProfileId(slot.GetProfileId());
        //load game
        DataManager.Instance.LoadGame();
        //load scene
        SceneManager.LoadSceneAsync(DataManager.Instance.gameData.lastScene);
    }
}
