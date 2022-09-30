using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializablePlayerStat
{
    public float totalLife;
    public float actualLife;
    public float maxRust;
    public float rust;
    public float intelligence;
    public float resistance;
    public float maxSprintTime;
    public int coins;
    public float exp;
    public int level;
    public SerializablePlayerStat(float totalLife, float actualLife, float maxRust, float rust, float intelligence, float resistance, float maxSprintTime, int coins, float exp, int level)
    {
        this.totalLife = totalLife;
        this.actualLife = actualLife;
        this.maxRust = maxRust;
        this.rust = rust;
        this.intelligence = intelligence;
        this.resistance = resistance;
        this.maxSprintTime = maxSprintTime;
        this.coins = coins;
        this.exp = exp;
        this.level = level;
    }
}
