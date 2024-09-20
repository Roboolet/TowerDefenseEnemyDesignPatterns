using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPooledObject
{
    public bool IsInUse { get; set; }

    public Dictionary<Transform, float> weightedTargets = new Dictionary<Transform, float>();

    private List<IEnemyDecorator> decorators;
    private ScratchPad scratchPad;

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
            decorators.Add(obj);
        }

        // create scratchpad
        scratchPad = new ScratchPad();
        for(int i = 0; i < _info.initData.Length; i++)
        {
            ScratchPadInitData iota = _info.initData[i];
            scratchPad.Set(iota.key, iota.value);
        }

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

    }

    private void AddDecorator<T>() where T : IEnemyDecorator
    {

    }
}
