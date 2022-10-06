using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerModifiers : ScriptableObject
{
    public abstract void StatModifier(float value);
}
