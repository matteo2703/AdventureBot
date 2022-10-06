using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    private Image expBar;

    private void Awake()
    {
        expBar = GetComponent<Image>();
    }

    private void OnEnable()
    {
        SetExp();
    }
    public void SetExp()
    {
        expBar.fillAmount = PlayerStats.Instance.exp / PlayerStats.Instance.ExpNeeded();
    }
}
