using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    public EnemyInfoElement[] decorators;
    public ScratchPadInitData[] initData;
}

[System.Serializable]
public struct EnemyInfoElement
{
    public EnemyInfoComponentSelector type;
}

public enum EnemyInfoComponentSelector
{
    // names must exactly match their corresponding class, but with the "-Decorator" suffix removed
    CastleTargeter
}


