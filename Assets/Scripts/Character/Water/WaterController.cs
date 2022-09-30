using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    PlayerLocomotion playerLocomotion;
    Rigidbody playerRigidbody;
    PlayerStats playerStats;
    private void Awake()
    {
        playerLocomotion = FindObjectOfType<PlayerLocomotion>(true);
        playerRigidbody = FindObjectOfType<Rigidbody>(true);
        playerStats = FindObjectOfType<PlayerStats>(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerLocomotion.isSwimming = true;
            playerStats.rugModifier = 5f;
            playerRigidbody.useGravity = false;   
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerLocomotion.isSwimming = false;
            playerStats.rugModifier = 1f;
            playerRigidbody.useGravity = true;
        }
    }
}
