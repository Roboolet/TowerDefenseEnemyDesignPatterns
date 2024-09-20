using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/WaveInfo")]
public class WaveInfo : ScriptableObject
{
    public int waveId;
    public WaveInfoElement[] waveElements;
}

public struct WaveInfoElement
{
    public string enemyTypeName;
    public int spawnAmount;
    public WaveSpawnpointPreference spawnpointPreference;
}

public enum WaveSpawnpointPreference
{
    RANDOM,
    RANDOM_SEQUENTIAL,
    ROUND_ROBIN,
}