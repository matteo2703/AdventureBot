using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableCamera
{
    public SerializableObjectPosition camera;
    public float lookAngle;
    public float pivotAngle;

    public SerializableCamera(SerializableObjectPosition camera, float lookAngle, float pivotAngle)
    {
        this.camera = camera;
        this.lookAngle = lookAngle;
        this.pivotAngle = pivotAngle;
    }
}
