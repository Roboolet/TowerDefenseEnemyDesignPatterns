using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPooledObject
{
    public bool IsInUse { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Update()
    {
        
    }

    public void Initialize(EnemyInfo _info)
    {
        Activate();
    }

    public void Activate()
    {
        IsInUse = true;
    }

    public void Deactivate()
    {
        IsInUse = false;
        ResetDecorators();
    }

    private void ResetDecorators()
    {

    }

    private void AddDecorator<T>() where T : IEnemyDecorator
    {

    }
}
