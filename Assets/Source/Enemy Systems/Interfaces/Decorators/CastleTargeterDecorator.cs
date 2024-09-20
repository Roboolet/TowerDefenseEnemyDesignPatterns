using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleTargeterDecorator : AEnemyDecorator
{
    public override void OnUpdate(ScratchPad<Enemy> _scratchPad)
    {
        Debug.Log("Woah, i'm doing stuff!! " + _scratchPad.Get<int>("towerWeight"));
    }
}
