using UnityEngine;

public class Panel : MonoBehaviour
{
    public bool active;

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
    public void Activate()
    {
        gameObject.SetActive(active);
    }
}
