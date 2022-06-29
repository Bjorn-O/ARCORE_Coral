using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static ObjectPool<GameObject> EnemyPool;
    public Enemy enemyToSpawn;
    public int amountToPool = 20;
    public EnemySpawner[] spawners;

    private void Awake()
    {
        EnemyPool = new ObjectPool<GameObject>(() =>
        {
            return SpawnLocation().InitiateEnemy();
        }, enemy =>
        {
            enemy.gameObject.SetActive(true);
        }, enemy => 
        {
            enemy.gameObject.SetActive(false);        
        }, enemy =>
        {
            Destroy(enemy.gameObject);
        },false, amountToPool, amountToPool + 5 
            );
    }

    private EnemySpawner SpawnLocation()
    {
        var i = Random.Range(0, 3);
        return spawners[i];
    }
}
