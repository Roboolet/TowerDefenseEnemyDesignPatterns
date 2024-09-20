using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ScratchPad<T>
{
    public T Owner { get; private set; }
    private Dictionary<string, string> data = new Dictionary<string, string>();

    public ScratchPad(T _owner, ScratchPadInitData[] _initData)
    {
        Owner = _owner; 
        for (int i = 0; i < _initData.Length; i++)
        {
            Set(_initData[i].key, _initData[i].value);
        }
    }

    public void Set(string key, string value)
    {
        if(data.ContainsKey(key)) { data.Remove(key); }
        data.Add(key, value);
    }

    public T Get<T>(string key)
    {
        if (data.ContainsKey(key))
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter.CanConvertFrom(data[key].GetType()))
            {
                return (T)converter.ConvertFrom(data[key]);
            }
            else
            {
                Debug.LogError("ScratchPad could not convert value from key " + key + " to type "+ typeof(T).Name);
                return default(T);
            }
        }
        else
        {
            Debug.LogError("Scratchpad does not contain entry for key: " + key);
            return default(T);
        }
    }

    public string Get(string key)
    {
        if (data.ContainsKey(key))
        {
            return data[key];
        }
        else
        {
            Debug.LogError("Scratchpad does not contain entry for key: " + key);
            return "";
        }
    }

    public void Clear()
    {
        data.Clear();
    }

    void Initialize()
    {

    }

}

[System.Serializable]
public struct ScratchPadInitData
{
    public string key;
    public string value;
}