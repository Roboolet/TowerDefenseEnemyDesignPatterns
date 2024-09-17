using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSystem : MonoBehaviour
{
    public ObjectPool enemyPool;

    [SerializeField] WaveInfo[] _waves;
    [SerializeField] EnemySpawnpoint[] spawners;
    [SerializeField] EnemyInfo[] _enemyTypes;

    private Dictionary<string, EnemyInfo> enemyTypes = new Dictionary<string, EnemyInfo>();
    private Dictionary<int, WaveInfo> waves = new Dictionary<int, WaveInfo>();
    private int currentWave;
    private WaveInfo currentWaveInfo;

    private void Awake()
    {
        foreach(EnemyInfo info in _enemyTypes)
        {
            enemyTypes.Add(info.name, info);
        }
    }

    public void StartWave(int _waveNumber)
    {
        Debug.Log("Starting wave " + _waveNumber);
        currentWave = _waveNumber;

        // get wave info
        if (waves.ContainsKey(_waveNumber))
        {
            currentWaveInfo = waves[_waveNumber];
        }
        else
        {
            Debug.LogWarning("No WaveInfo found with index " + _waveNumber + ". Generating new WaveInfo");
            currentWaveInfo = GenerateUniqueWaveInfo(_waveNumber);
        }

        // enqueue enemies to spawn points
        foreach(WaveInfoElement elem in currentWaveInfo.waveElements)
        {
            // skip this loop if enemy type does not exist
            if (!enemyTypes.TryGetValue(elem.enemyTypeName, out EnemyInfo elemInfo)) 
            {
                Debug.LogError("Could not enqueue enemy with name " + elem.enemyTypeName + ". Key does not exist in dictionary");
                continue;
            }

            switch (elem.spawnpointPreference)
            {
                default:
                case WaveSpawnpointPreference.ROUND_ROBIN:
                    for (int i = 0; i < elem.spawnAmount; i++)
                    {
                        spawners[i % spawners.Length].AddToQueue(elemInfo);
                    }
                    break;

                case WaveSpawnpointPreference.RANDOM:
                    for (int i = 0; i < elem.spawnAmount; i++)
                    {
                        GetRandomSpawnpoint().AddToQueue(elemInfo);
                    }
                    break;

                case WaveSpawnpointPreference.RANDOM_SEQUENTIAL:
                    EnemySpawnpoint randomSpawn = GetRandomSpawnpoint();
                    for(int i = 0; i < elem.spawnAmount; i++)
                    {
                        randomSpawn.AddToQueue(elemInfo);
                    }
                    break;                
            }
        }
    }

    EnemySpawnpoint GetRandomSpawnpoint()
    {
        return spawners[Random.Range(0, spawners.Length)];
    }

    WaveInfo GenerateUniqueWaveInfo(int _waveNumber)
    {
        return new WaveInfo();
    }

}
