using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemyDecorator : IEnemyDecorator
{
    public virtual void OnSpawn(ScratchPad _scratchPad)
    {
    }
    public virtual void OnUpdate(ScratchPad _scratchPad)
    {

    }

    public virtual void OnDeath(ScratchPad _scratchPad)
    {

    }


    
}
