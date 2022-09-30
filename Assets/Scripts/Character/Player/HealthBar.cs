using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    public PlayerStats stats;

    private void Awake()
    {
        healthBar = GetComponent<Image>();
        stats = FindObjectOfType<PlayerStats>(true);
    }

    private void OnEnable()
    {
        SetHealth();
    }
    public void SetHealth()
    {
        healthBar.fillAmount = stats.actualLife / stats.totalLife;
    }
}
