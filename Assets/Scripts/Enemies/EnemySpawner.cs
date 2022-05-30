using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private Transform myTransform;
    private GameObject myGameOjbect;
    private bool _isSpawning = false;
    
    [Header("Components")]
    [SerializeField] private GameObject enemyToSpawn;
    
    [Header("Spawn Timer")]
    [SerializeField] private float initialDelay;
    [SerializeField] private float spawnDelay;

    [Header("SpawnDirection")] 
    [SerializeField] private float spawnDirection;
    [SerializeField] private float spawnAngle;
    [SerializeField] private float spawnRangeMax;
    [SerializeField] private float spawnRangeMin;
    private void Awake()
    {
        this.myTransform = transform;
        this.myGameOjbect = gameObject;
    }

    private void InitiateEnemy()
    {
        var enemy = Instantiate(enemyToSpawn, GetRandomSpawnPoint(), Quaternion.identity);
        enemy.transform.LookAt(myTransform);
    }

    public void InitiateSpawning()
    {
        InvokeRepeating(nameof(InitiateEnemy),initialDelay, spawnDelay);
    }

    public void DisableSpawning()
    {
        CancelInvoke(nameof(InitiateEnemy));
    }
    
    private Vector3 GetRandomSpawnPoint()
    {
        float randAngle = Random.Range((-spawnAngle / 2) + spawnDirection,(spawnAngle / 2)+ spawnDirection);
        print(randAngle);
        var randRad = randAngle * Mathf.PI / 180;
        var randDist = Random.Range(spawnRangeMin, spawnRangeMax);
 
        return myTransform.position + new Vector3(Mathf.Sin(randRad),0 ,Mathf.Cos(randRad))*randDist;
    }

    void OnDrawGizmosSelected()
    {
        float halfFOV = spawnAngle / 2.0f;
        
        Quaternion upRayRotation = Quaternion.AngleAxis(-halfFOV + spawnDirection, Vector3.up);
        Quaternion downRayRotation = Quaternion.AngleAxis(halfFOV + spawnDirection, Vector3.up);

        Vector3 spawnIndicatorLeft = upRayRotation * transform.forward * spawnRangeMax;
        Vector3 spawnIndicatorRight = downRayRotation * transform.forward * spawnRangeMax;
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, spawnIndicatorLeft);
        Gizmos.DrawRay(transform.position, spawnIndicatorRight);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, spawnRangeMax);
        
        Vector3 safeIndicatorLeft = upRayRotation * transform.forward * spawnRangeMin;
        Vector3 safeIndicatorRight = downRayRotation * transform.forward * spawnRangeMin;
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, safeIndicatorLeft);
        Gizmos.DrawRay(transform.position, safeIndicatorRight);
        Gizmos.DrawWireSphere(this.transform.position, spawnRangeMin);
    }
}
