using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;
    public bool removingHealth;
    public PlayerStats stats;

    private void Awake()
    {
        stats = FindObjectOfType<PlayerStats>(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !removingHealth)
            StartCoroutine(RemoveHealth());
    }

    public IEnumerator RemoveHealth()
    {
        stats.settingHealth = true;
        removingHealth = true;
        stats.SetHealth(-damage);
        yield return new WaitForSeconds(0.5f);
        removingHealth = false;
        stats.settingHealth = false;
    }
}
