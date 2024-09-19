using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemyDecorator : IEnemyDecorator
{
    protected Enemy Owner {  get; private set; }

    public virtual void OnSpawn(Enemy _owner)
    {
        Owner = _owner;
    }
    public virtual void OnUpdate()
    {

    }

    public virtual void OnDeath()
    {

    }


    
}
