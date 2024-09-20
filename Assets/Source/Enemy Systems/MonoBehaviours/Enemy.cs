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

    private List<IEnemyComponent> components = new List<IEnemyComponent>();
    private ScratchPad<Enemy> scratchPad;

    private void Update()
    {
        // call OnUpdate on all components
        for (int i = 0; i < components.Count; i++)
        {
            components[i].OnUpdate(scratchPad);
        }

        // do things
    }

    public void Initialize(EnemyInfo _info)
    {
        Activate();

        // create all components
        for(int i = 0; i < _info.components.Length; i++)
        {
            // dynamically create component instance from a an enum with the name of the type
            Type decoType = Type.GetType(_info.components[i].ToString()+"Component");
            IEnemyComponent obj = (IEnemyComponent)Activator.CreateInstance(decoType);
            AddDecorator(obj);
        }

        // create scratchpad
        scratchPad = new ScratchPad<Enemy>(this, _info.initData);        

        // call OnSpawn on all components
        for (int i = 0; i < components.Count; i++)
        {
            components[i].OnSpawn(scratchPad);
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
        components.Clear();
        scratchPad.Clear();
        weightedTargets.Clear();
    }

    private void AddDecorator<T>(T obj) where T : IEnemyComponent
    {
        components.Add(obj);
    }

    public void TakeDamage(float _damage)
    {
        Health -= _damage;
        OnHealthChanged?.Invoke();
    }
}
