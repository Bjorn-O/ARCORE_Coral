using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject deathExplosion;
    
    [Header("Variables")]
    [SerializeField] [Range(0, 5)] private float destroyTime;

    public void DestroyThis()
    {
        Destroy(this.gameObject, destroyTime);
    }
    
    private void OnDestroy()
    {
        if (deathExplosion != null)
        { 
            var kaboom = Instantiate(deathExplosion);
            SpawnManager.EnemyPool.Release(gameObject);
        }
    }
}