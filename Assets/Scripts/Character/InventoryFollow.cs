using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryFollow : MonoBehaviour
{
    [SerializeField] GameObject targetFollow;

    void Update()
    {
        transform.position = new(targetFollow.transform.position.x, targetFollow.transform.position.y, targetFollow.transform.position.z);
    }
}