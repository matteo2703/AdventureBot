using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    private void Awake()
    {
        playerRigidbody = FindObjectOfType<Rigidbody>(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLocomotion.Instance.isSwimming = true;
            PlayerStats.Instance.rugModifier = 5f;
            playerRigidbody.useGravity = false;   
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLocomotion.Instance.isSwimming = false;
            PlayerStats.Instance.rugModifier = 1f;
            playerRigidbody.useGravity = true;
        }
    }
}
