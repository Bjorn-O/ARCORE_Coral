using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [Header("Object Pool Settings")]
    public static ObjectPool<GameObject> EnemyPool;
    [SerializeField] private EnemySpawner[] spawners;
    [SerializeField] [Range(0,100)] private int amountToPool = 20;
    
    private bool _shouldSpawn = false;
    private float _spawnTimer = 0;
    private int _spawnLevel = 0;
    
    [SerializeField] private float[] spawnIntervals = new float[4];

    private void Start()
    {
        InitiatePool();
    }

    private void Update()
    {
        if (_shouldSpawn) SpawnTimer();
    }


    public void InitiateSpawning()
    {
        _shouldSpawn = true;
    }

    private void SpawnTimer()
    {
        if (_spawnTimer <= 0) _spawnTimer = spawnIntervals[0];
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0) Spawn();
    }

    private void Spawn()
    {
        EnemyPool.Get();
        
    }

    private void InitiatePool()
    {
        EnemyPool = new ObjectPool<GameObject>(() =>
            {
                Debug.Log("before the mistake 1");
                return SpawnLocation().InitiateEnemy();
            }, enemy =>
            {
                Debug.Log("before the mistake 2");
                enemy.gameObject.SetActive(true);
            }, enemy => 
            {
                Debug.Log("before the mistake 3");
                enemy.gameObject.SetActive(false);        
            }, enemy =>
            {
                Debug.Log("before the mistake 4");
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
