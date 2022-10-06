using UnityEngine;

[CreateAssetMenu(fileName ="HealtModifier", menuName = "Player Modifiers/Health Modifier")]
public class HealthModifier : PlayerModifiers
{
    public override void StatModifier(float value)
    {
        PlayerStats.Instance.SetHealth(value);
    }
}
