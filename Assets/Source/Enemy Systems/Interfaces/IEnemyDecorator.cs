using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyDecorator
{
    public void OnSpawn(Enemy _owner);
    public void OnUpdate();
    public void OnDeath();
}
