using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite itemIcon;

    public int value;

    public bool isStackable;
    public ItemTypes itemType;
}

public enum ItemTypes
{
    coin,
    key,
    usable,
    collectable,
}