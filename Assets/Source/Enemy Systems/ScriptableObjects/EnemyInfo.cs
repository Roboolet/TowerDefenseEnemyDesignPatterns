using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    public EnemyInfoComponentSelector[] components;
    public ScratchPadInitData[] initData;
}

public enum EnemyInfoComponentSelector
{
    // names must exactly match their corresponding class, but with the "-Component" suffix removed
    CastleTargeter
}


