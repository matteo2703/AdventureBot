using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    [SerializeField] GameObject targetFollow;

    public static MinimapFollow Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Update()
    {
        transform.position = new(targetFollow.transform.position.x, targetFollow.transform.position.y + 10, targetFollow.transform.position.z);
    }
}
