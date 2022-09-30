using UnityEngine;

[CreateAssetMenu(fileName ="HealtModifier", menuName = "Player Modifiers/Health Modifier")]
public class HealthModifier : PlayerModifiers
{
    public override void StatModifier(PlayerStats stats, float value)
    {
        stats.SetHealth(value);
    }
}
