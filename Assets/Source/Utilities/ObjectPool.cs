using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : IPooledObject
{
    private List<T> activatedObjects = new List<T>();
    private Queue<T> sleepingObjects = new Queue<T>();

    public bool TryActivateObject(out T _obj)
    {
        CleanActivePool();
        if (sleepingObjects.TryDequeue(out T _sleepingDq))
        {
            _obj = _sleepingDq;
            _obj.Activate();
            return true;
        }

        // if all else fails
        _obj = default(T);
        return false;
    }

    public void AddObjectToPool(T _obj)
    {
        if (_obj.IsInUse) { activatedObjects.Add(_obj); }
        else { sleepingObjects.Enqueue(_obj); }
    }

    public void CleanActivePool()
    {
        for (int i = activatedObjects.Count - 1; i >= 0; i--)
        {
            if (!activatedObjects[i].IsInUse)
            {
                T unusedObj = activatedObjects[i];
                sleepingObjects.Enqueue(unusedObj);
                unusedObj.Deactivate();
                activatedObjects.RemoveAt(i);
            }
        }
    }
}
