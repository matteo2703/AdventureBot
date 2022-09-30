using System.Collections.Generic;
using UnityEngine;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] List<GameObject> pages;
    [SerializeField] GameObject firstPage;

    private void Awake()
    {
        foreach (var page in pages)
            page.SetActive(false);

        firstPage.SetActive(true);
    }
}
