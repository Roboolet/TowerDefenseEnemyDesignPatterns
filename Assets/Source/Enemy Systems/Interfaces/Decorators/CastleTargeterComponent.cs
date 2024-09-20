using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleTargeterComponent : AEnemyComponent
{
    public override void OnUpdate(ScratchPad<Enemy> _scratchPad)
    {
        Debug.Log("Woah, i'm doing stuff!! " + _scratchPad.Get<int>("towerWeight"));
    }
}
