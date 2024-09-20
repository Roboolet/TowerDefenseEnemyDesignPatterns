using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyDecorator
{
    public void OnSpawn(ScratchPad _scratchPad);
    public void OnUpdate(ScratchPad _scratchPad);
    public void OnDeath(ScratchPad _scratchPad);
}
