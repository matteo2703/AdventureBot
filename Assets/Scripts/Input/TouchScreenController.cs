using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreenController : Panel
{
    private void Awake()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
            active = true;
        else
            active = false;

        gameObject.SetActive(active);
    }
}
