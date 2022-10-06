using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool locked;
    public bool open;

    [SerializeField] InventoryItem key;

    private void Awake()
    {
        locked = true;
        open = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!open)
        {
            if (other.CompareTag("Player"))
            {
                if (ItemsManager.Instance.FindItemInInventory(key))
                {
                    ItemsManager.Instance.SubtractPlayerObject(key);
                    locked = false;
                    StartCoroutine(Opening());
                }
            }
        }
    }
    public IEnumerator Opening()
    {
        if (!locked)
        {
            for (int i = 0; i < 90; i++)
            {
                yield return new WaitForSeconds(0.01f);
                gameObject.transform.Rotate(0, 0, 1);
            }
            open = true;
        }
    }
}
