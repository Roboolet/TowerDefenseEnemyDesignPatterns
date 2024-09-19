using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSystem : MonoBehaviour
{
    [SerializeField] float nextWaveDelay;
    [SerializeField] WaveInfo[] _waves;
    [SerializeField] EnemySpawnpoint[] spawners;
    [SerializeField] EnemyInfo[] _enemyTypes;

    private Dictionary<string, EnemyInfo> enemyTypes = new Dictionary<string, EnemyInfo>();
    private Dictionary<int, WaveInfo> waves = new Dictionary<int, WaveInfo>();
    private int currentWave = 0;
    private WaveInfo currentWaveInfo;

    private void Awake()
    {
        foreach(EnemyInfo info in _enemyTypes)
        {
            enemyTypes.Add(info.name, info);
        }

        foreach (WaveInfo info in _waves)
        {
            waves.Add(info.waveId, info);
        }
    }

    private IEnumerator WaveStartingLoop()
    {
        // instead of using WaitForSeconds and using regular scaled time, there should be a seperate "game-time" variable used by enemies and towers
        yield return new WaitForSecondsRealtime(nextWaveDelay);
        StartWave(currentWave + 1);
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

        StartCoroutine(WaveStartingLoop());
    }

    private EnemySpawnpoint GetRandomSpawnpoint()
    {
        return spawners[Random.Range(0, spawners.Length)];
    }

    private WaveInfo GenerateUniqueWaveInfo(int _waveNumber)
    {
        return new WaveInfo();
    }

    
}
