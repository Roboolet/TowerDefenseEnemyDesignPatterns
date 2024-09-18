using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnpoint : MonoBehaviour
{
    static ObjectPool<Enemy> enemyPool;

    [SerializeField] float spawnsPerSecond = 2;

    Queue<EnemyInfo> spawnQueue = new Queue<EnemyInfo>();
    float lastSpawnTimestamp;

    // this is loaded from the root\Resources folder
    const string enemyPrefabPath = "EnemyInstance";
    GameObject enemyPrefab;

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

    Enemy SpawnEnemy(EnemyInfo _enemyInfo)
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

        // TODO:
        // set enemy stats
        // add correct enemy decorators

        return newEnemy;
    }
}
