using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnpoint : MonoBehaviour
{
    private static ObjectPool<Enemy> enemyPool;

    [SerializeField] float spawnsPerSecond = 2;

    private Queue<EnemyInfo> spawnQueue = new Queue<EnemyInfo>();
    private float lastSpawnTimestamp;

    // this is loaded from the root\Resources folder
    private const string enemyPrefabPath = "EnemyInstance";
    private GameObject enemyPrefab;

    private void Awake()
    {
        // create enemypool
        if (enemyPool == null)
        {
            enemyPool = new ObjectPool<Enemy>();
        }
        enemyPrefab = Resources.Load<GameObject>(enemyPrefabPath);
    }

    private void Update()
    {
        if(spawnQueue.Count == 0) { return; }

        // spawn the enemies over time
        if(lastSpawnTimestamp + (1/spawnsPerSecond) < Time.time)
        {
            lastSpawnTimestamp = Time.time;
            SpawnEnemy(spawnQueue.Dequeue());
        }
    }

    public void AddToQueue(EnemyInfo _enemyInfo)
    {
        spawnQueue.Enqueue(_enemyInfo);
    }

    private Enemy SpawnEnemy(EnemyInfo _enemyInfo)
    {
        Enemy newEnemy;
        if (enemyPool.TryActivateObject(out Enemy _newEnemy))
        {
            // get existing instance from object pool and repurpose it
            newEnemy = _newEnemy;
        }
        else
        {
            // create new instance, adding it to the object pool
            newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
            enemyPool.AddObjectToPool(newEnemy);
        }

        newEnemy.Initialize(_enemyInfo);

        return newEnemy;
    }
}
