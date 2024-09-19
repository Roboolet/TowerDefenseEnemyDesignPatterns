using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    public bool IsInUse { get; set; }

    /// <summary>
    /// Called when this IPooledObject is being requested by the ObjectPool
    /// </summary>
    public void Activate();

    /// <summary>
    /// Called when this IPooledObject is being relinquished to the ObjectPool
    /// </summary>
    public void Deactivate();
}
