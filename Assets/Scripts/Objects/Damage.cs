using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;
    public bool removingHealth;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !removingHealth)
            StartCoroutine(RemoveHealth());
    }

    public IEnumerator RemoveHealth()
    {
        PlayerStats.Instance.settingHealth = true;
        removingHealth = true;
        PlayerStats.Instance.SetHealth(-damage);
        yield return new WaitForSeconds(0.5f);
        removingHealth = false;
        PlayerStats.Instance.settingHealth = false;
    }
}
