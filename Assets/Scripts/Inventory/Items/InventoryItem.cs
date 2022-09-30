using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemStates { inWorld, destroyed}
public class InventoryItem : MonoBehaviour
{
    [SerializeField] public Item item;
    public int id;
    public ItemStates state;
    [SerializeField] public PlayerModifiers modifier;

    PlayerStats playerStats;
    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (item.itemType == ItemTypes.usable || item.itemType == ItemTypes.collectable)
            {
                ItemsManager.Instance.AddPlayerObject(this);
                SetState(ItemStates.destroyed);
            }
            else if (item.itemType == ItemTypes.coin)
            {
                Use();
                SetState(ItemStates.destroyed);
            }
        }
    }
    public void SetState(ItemStates newState)
    {
        state = newState;
        CheckState();
    }
    public void CheckState()
    {
        switch (state)
        {
            case ItemStates.destroyed:
                gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    public void TryToUse()
    {
        if (item.itemType == ItemTypes.usable)
            Use();
    }
    private void Use()
    {
        modifier.StatModifier(playerStats, item.value);
    }
}
