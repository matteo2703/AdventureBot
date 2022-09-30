using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keys = new();
    [SerializeField] private List<TValue> values = new();
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach(KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }
    public void OnAfterDeserialize()
    {
        Clear();

        if (keys.Count != values.Count)
            Debug.Log("Errore nella deserializzazione dei dati, le chiavi e i valori non corrispondono");

        for(int i = 0; i < Keys.Count; i++)
        {
            Add(keys[i], values[i]);
        }
    }
}
