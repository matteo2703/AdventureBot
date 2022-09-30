using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDataManager
{
    private InputManager inputManager;
    private AnimatorManager animatorManager;
    private bool addLife;
    [HideInInspector] public bool settingHealth;
    private IEnumerator coroutine;

    [Header("Player stats")]
    public float totalLife;
    public float actualLife;

    public float maxRust;
    public float rust;

    public float exp = 0;
    public int level = 1;
    private float expAmuountMoltiplier = 0.2f;
    private float expIncreasingMultiplier = 2f;

    public float intelligence;
    public float resistance;
    public float maxSprintTime;

    public int coins;

    [SerializeField] HealthBar outsideHealthBar;
    [SerializeField] HealthBar inventoryHealthBar;
    [SerializeField] RustBar inventoryRustBar;
    [SerializeField] TextMeshProUGUI levelText;
    public float addAbilityValue = 0.001f;
    public float rugModifier;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        animatorManager = GetComponent<AnimatorManager>();
        rugModifier = 1f;
    }
    private void Update()
    {
        maxSprintTime = 5f + resistance;

        if(!addLife && !settingHealth && actualLife < totalLife)
        {
            coroutine = Wait(5f);
            StartCoroutine(coroutine);
        }
        else if(settingHealth || actualLife >= totalLife || inputManager.moveAmount != 0)
        {
            if(coroutine != null)
                StopCoroutine(coroutine);
            addLife = false;
        }

        if (addLife)
            SetHealth(0.01f);
    }
    public void Rusting()
    {
        if(rust < maxRust)
            rust += 0.1f * rugModifier;
    }
    public void MoreResistance()
    {
        resistance += addAbilityValue;
    }
    public void MoreIntelligence()
    {
        intelligence += addAbilityValue;
    }
    public void AddExp(float value)
    {
        float expNeeded = ExpNeeded() - exp;

        if (value > expNeeded)
        {
            level++;
            AddExp(value - expNeeded);
        }
        else
            exp += value;

    }
    public float ExpNeeded()
    {
        float levelPlusBase = (level + 1) / expAmuountMoltiplier;
        float levelNowBase = level / expAmuountMoltiplier;

        float levelPlusExp = Mathf.Pow(levelPlusBase, expIncreasingMultiplier);
        float levelNowExp = Mathf.Pow(levelNowBase, expIncreasingMultiplier);

        return Mathf.Floor(levelPlusExp - levelNowExp);
    }
    private IEnumerator Wait(float value)
    {
        yield return new WaitForSeconds(value);
        if (!addLife && !settingHealth && actualLife < totalLife)
            addLife = true;
        else if(settingHealth || actualLife >= totalLife || inputManager.moveAmount != 0)
            addLife = false;
    }
    public void SetHealth(float value)
    {
        actualLife += value;
        if (actualLife > totalLife)
            actualLife = totalLife;
        else if (actualLife < 0)
            actualLife = 0;

        outsideHealthBar.SetHealth();
        if(inventoryHealthBar.isActiveAndEnabled)
            inventoryHealthBar.SetHealth();
        if (inventoryRustBar.isActiveAndEnabled)
            inventoryRustBar.SetRust();
    }
    public void SetRust(float value)
    {
        rust += value;
        if (rust > maxRust)
            rust = maxRust;
        else if (rust < 0)
            rust = 0;

        animatorManager.slowFactor = Mathf.Clamp(rust, 0f, 99f) / 100;
    }
    public void SetCoins(int value)
    {
        coins += value;
    }
    public void LoadGame(GenericGameData data)
    {
        totalLife = data.playerStat.totalLife;
        actualLife = data.playerStat.actualLife;
        maxRust = data.playerStat.maxRust;
        rust = data.playerStat.rust;
        coins = data.playerStat.coins;
        resistance = data.playerStat.resistance;
        maxSprintTime = data.playerStat.maxSprintTime;
        intelligence = data.playerStat.intelligence;
        exp = data.playerStat.exp;
        level = data.playerStat.level;

        outsideHealthBar.SetHealth();
        animatorManager.slowFactor = Mathf.Clamp(rust, 0f, 99f) / 100;
        inputManager.sprintTime = data.playerStat.maxSprintTime;
    }

    public void SaveGame(GenericGameData data)
    {
        data.playerStat.totalLife = totalLife;
        data.playerStat.actualLife = actualLife;
        data.playerStat.maxRust = maxRust;
        data.playerStat.rust = rust;
        data.playerStat.coins = coins;
        data.playerStat.resistance = resistance;
        data.playerStat.maxSprintTime = maxSprintTime;
        data.playerStat.intelligence = intelligence;
        data.playerStat.exp = exp;
        data.playerStat.level = level;
    }
}
