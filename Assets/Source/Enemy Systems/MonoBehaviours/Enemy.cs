using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPooledObject, IHealthHaver, IDamageable
{
    public bool IsInUse { get; set; }
    public float Health { get; set; }
    public event Action OnHealthChanged;

    public Dictionary<Transform, float> weightedTargets = new Dictionary<Transform, float>();

    private List<IEnemyDecorator> decorators = new List<IEnemyDecorator>();
    private ScratchPad<Enemy> scratchPad;

    private void Update()
    {
        // call OnUpdate on all decorators
        for (int i = 0; i < decorators.Count; i++)
        {
            decorators[i].OnUpdate(scratchPad);
        }

        // do things
    }

    public void Initialize(EnemyInfo _info)
    {
        Activate();

        // create all decorators
        for(int i = 0; i < _info.decorators.Length; i++)
        {
            // dynamically create decorator instance from a an enum with the name of the type
            Type decoType = Type.ReflectionOnlyGetType(_info.decorators[i].type.ToString()+"Decorator", true, false);
            IEnemyDecorator obj = (IEnemyDecorator)Activator.CreateInstance(decoType);
            AddDecorator(obj);
        }

        // create scratchpad
        scratchPad = new ScratchPad<Enemy>(this, _info.initData);        

        // call OnSpawn on all decorators
        for (int i = 0; i < decorators.Count; i++)
        {
            decorators[i].OnSpawn(scratchPad);
        }
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
        decorators.Clear();
        scratchPad.Clear();
        weightedTargets.Clear();
    }

    private void AddDecorator<T>(T obj) where T : IEnemyDecorator
    {
        decorators.Add(obj);
    }

    public void TakeDamage(float _damage)
    {
        Health -= _damage;
        OnHealthChanged?.Invoke();
    }
}
