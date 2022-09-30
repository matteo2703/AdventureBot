using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    private Image expBar;
    public PlayerStats stats;

    private void Awake()
    {
        expBar = GetComponent<Image>();
        stats = FindObjectOfType<PlayerStats>(true);
    }

    private void OnEnable()
    {
        SetExp();
    }
    public void SetExp()
    {
        expBar.fillAmount = stats.exp / stats.ExpNeeded();
    }
}
