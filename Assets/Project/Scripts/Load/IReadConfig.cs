﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class IReadConfig<TKey, TValue>
{
    private Dictionary<TKey, TValue> mDict = null;

    protected abstract void LoadData(Dictionary<TKey, TValue> dict);

    public void Load(Dictionary<TKey, TValue> dict)
    {
        if (dict == null) return;
        if (mDict == null) 
            mDict = dict;       
            LoadData(dict);         
       
    }

    public TValue GetDataById(TKey key)
    {
        if (mDict == null)
        {
            return default(TValue);
        }
        TValue obj = default(TValue);
        mDict.TryGetValue(key, out obj);
        return obj;
    }
}

public abstract class DBModule
{
    public abstract int GetTypeId();
}
