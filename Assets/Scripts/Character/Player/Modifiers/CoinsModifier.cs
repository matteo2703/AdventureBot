using UnityEngine;

[CreateAssetMenu(fileName = "CoinsModifier", menuName = "Player Modifiers/Coins Modifier")]
public class CoinsModifier : PlayerModifiers
{
    public override void StatModifier(float value)
    {
        PlayerStats.Instance.SetCoins((int)value);
    }
}