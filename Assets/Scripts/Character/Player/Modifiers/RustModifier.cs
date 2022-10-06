using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealtModifier", menuName = "Player Modifiers/Rust Modifier")]
public class RustModifier : PlayerModifiers
{
    public override void StatModifier(float value)
    {
        PlayerStats.Instance.SetRust(-value);
    }
}
