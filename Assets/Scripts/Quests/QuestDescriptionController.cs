using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDescriptionController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;

    public void ActivePanel()
    {
        gameObject.SetActive(true);
    }

    public void DeactivePanel()
    {
        gameObject.SetActive(false);
    }

    public void SetDescription(Quest quest)
    {
        ActivePanel();

        title.text = quest.title;
        description.text = quest.description;
    }

    public void ResetDescription()
    {
        title.text = "";
        description.text = "";

        DeactivePanel();
    }
}