using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyDecorator
{
    public void OnSpawn(ScratchPad<Enemy> _scratchPad);
    public void OnUpdate(ScratchPad<Enemy> _scratchPad);
    public void OnDeath(ScratchPad<Enemy> _scratchPad);
}
