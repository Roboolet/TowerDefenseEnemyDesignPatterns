using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : IPooledObject
{
    public bool TryActivateObject(out T _obj)
    {
        _obj = default(T);
        return false;
    }

    public void AddObjectToPool(T _obj)
    {

    }
}
