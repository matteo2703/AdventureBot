using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableInventoryItems
{
    public int itemReferenceId;
    public int quantity;

    public SerializableInventoryItems(int id, int quantity)
    {
        itemReferenceId = id;
        this.quantity = quantity;
    }
}
