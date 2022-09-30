using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private TextMeshProUGUI time;

    [Header("Clear Data Button")]
    [SerializeField] private Button clearButton;

    public void SetData(GenericGameData data)
    {
        if (data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
            clearButton.gameObject.SetActive(false);
        }
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);
            clearButton.gameObject.SetActive(true);

            time.text = $"Anno {data.year} - Giorno {data.day}";
        }
    }

    public string GetProfileId()
    {
        return profileId;
    }
}
