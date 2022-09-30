using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RustBar : MonoBehaviour
{
    private Image rustBar;
    public PlayerStats stats;

    private void Awake()
    {
        rustBar = GetComponent<Image>();
        stats = FindObjectOfType<PlayerStats>(true);
    }

    private void OnEnable()
    {
        SetRust();
    }
    public void SetRust()
    {
        rustBar.fillAmount = stats.rust / stats.maxRust;
    }
}
