using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : IPooledObject
{
    private Queue<T> activatedObjects = new Queue<T>();
    private Queue<T> sleepingObjects = new Queue<T>();

    public bool TryActivateObject(out T _obj, bool allowActivatedObjects = false)
    {
        if (sleepingObjects.TryDequeue(out T _sleepingDq))
        {
            _obj = _sleepingDq;
            _obj.Activate();
            return true;
        }
        else if (allowActivatedObjects && activatedObjects.TryDequeue(out T _activatedDq))
        {
            _obj = _activatedDq;
            _obj.Deactivate();
            _obj.Activate();
            return true;
        }

        // if all else fails
        _obj = default(T);
        return false;
    }

    public void AddObjectToPool(T _obj)
    {
    }

    public void CleanPool(bool alsoCleanActiveObjects)
    {
        sleepingObjects.Clear();
        if(alsoCleanActiveObjects)
        {
            activatedObjects.Clear();
        }
    }
}
