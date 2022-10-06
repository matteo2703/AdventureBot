using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RustBar : MonoBehaviour
{
    private Image rustBar;
    private void Awake()
    {
        rustBar = GetComponent<Image>();
    }

    private void OnEnable()
    {
        SetRust();
    }
    public void SetRust()
    {
        rustBar.fillAmount = PlayerStats.Instance.rust / PlayerStats.Instance.maxRust;
    }
}
