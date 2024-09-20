using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemyComponent : IEnemyComponent
{
    public virtual void OnSpawn(ScratchPad<Enemy> _scratchPad) { }
    public virtual void OnUpdate(ScratchPad<Enemy> _scratchPad) { }
    public virtual void OnDeath(ScratchPad<Enemy> _scratchPad) { }
}
