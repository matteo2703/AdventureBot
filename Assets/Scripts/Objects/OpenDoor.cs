using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            StartCoroutine(CloseDoor());
        }
    }
    public IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 90; i++)
        {
            yield return new WaitForSeconds(0.01f);
            gameObject.transform.Rotate(0, 0, 1);
        }
    }
}
