using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializeDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TKey> keyList = new List<TKey>();
    [SerializeField] private List<TValue> valueList = new List<TValue>();
    public void OnBeforeSerialize()
    {
        keyList.Clear();
        valueList.Clear();
        foreach(KeyValuePair<TKey, TValue> pair in this)
        {
            keyList.Add(pair.Key);
            valueList.Add(pair.Value);
        }
    }
    public void OnAfterDeserialize()
    {
        this.Clear();
        if(keyList.Count != valueList.Count)
        {
            Debug.LogError("Tried to desearlize a serializeableDictionary, but amount of keys did not match amount of values.");
        }
        for(int i = 0; i < keyList.Count; i++)
        {
            this.Add(keyList[i], valueList[i]);
        }
    }
}
