using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;
    private void OnEnable()
    {
        SetHealth();
    }
    public void SetHealth()
    {
        healthBar.fillAmount = PlayerStats.Instance.actualLife / PlayerStats.Instance.totalLife;
    }
}
