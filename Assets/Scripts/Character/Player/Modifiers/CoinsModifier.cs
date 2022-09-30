using UnityEngine;

[CreateAssetMenu(fileName = "CoinsModifier", menuName = "Player Modifiers/Coins Modifier")]
public class CoinsModifier : PlayerModifiers
{
    public override void StatModifier(PlayerStats stats, float value)
    {
        stats.SetCoins((int)value);
    }
}