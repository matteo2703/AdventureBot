using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkQuest : Quest
{
    private void OnTriggerEnter(Collider other)
    {
        if (inProgress && other.CompareTag("Player"))
            EndQuest();
    }
}
