using System;
using Input;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject enemyToSpawn;

    [Header("SpawnDirection")] 
    [Range(0, 360)]
    [SerializeField] private float spawnDirection = 0;
    [Range(0, 360)]
    [SerializeField] private float spawnAngle = 90;
    [SerializeField] private float spawnRangeMax = 1;
    [SerializeField] private float spawnRangeMin = 0.5f;

    [Header("TestValues")] 
    [SerializeField] private bool testBool;

    [Header("GizmoSettings")] 
    [SerializeField] private bool drawGizmos = true;
    [Range(0,360)]
    [SerializeField] private int edgeIndicators = 10;
    [Range(0.05f, 0.01f)]
    [SerializeField] private float edgeIndicatorSize = 0.025f;

    public bool testingOn;
    private Transform _myTransform;
    
    public GameObject InitiateEnemy()
    {
        var enemy = Instantiate(enemyToSpawn, GetRandomSpawnPoint(), Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().Target(this.transform);
        enemy.transform.LookAt(_myTransform);
        return enemy;
    }

    private Vector3 GetRandomSpawnPoint()
    {
        _myTransform ??= gameObject.transform; // hier was je fout! ik doe t niet hier ipv awake want je anders script runned eerder dan deze.
        var randAngle = Random.Range((-spawnAngle / 2) + spawnDirection,(spawnAngle / 2)+ spawnDirection);
        var randRad = randAngle * Mathf.PI / 180;
        var randDist = Random.Range(spawnRangeMin, spawnRangeMax);
        return _myTransform.position + new Vector3(Mathf.Sin(randRad),0 ,Mathf.Cos(randRad))*randDist;
    }

    void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;
        float halfFOV = spawnAngle / 2.0f;
        
        Quaternion upRayRotation = Quaternion.AngleAxis(-halfFOV + spawnDirection, Vector3.up);
        Quaternion downRayRotation = Quaternion.AngleAxis(halfFOV + spawnDirection, Vector3.up);

        Vector3 spawnIndicatorLeft = upRayRotation * transform.forward * spawnRangeMax;
        Vector3 spawnIndicatorRight = downRayRotation * transform.forward * spawnRangeMax;
        
        Gizmos.color = Color.magenta;
        var position = transform.position;
        Gizmos.DrawRay(position, spawnIndicatorLeft);
        Gizmos.DrawRay(position, spawnIndicatorRight);
        for (int i = 0; i < edgeIndicators; i++)
        {
            var offset = spawnAngle / edgeIndicators;
            Quaternion localRotation = Quaternion.AngleAxis(offset * i + spawnDirection - spawnAngle /2 + offset / 2, Vector3.up);
            var transform1 = transform;
            Vector3 localPosition = localRotation * transform1.forward * spawnRangeMax;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(localPosition + transform1.position, edgeIndicatorSize);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRangeMax);


        var forward1 = transform.forward;
        Vector3 safeIndicatorLeft = upRayRotation * forward1 * spawnRangeMin;
        Vector3 safeIndicatorRight = downRayRotation * forward1 * spawnRangeMin;
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, safeIndicatorLeft);
        Gizmos.DrawRay(transform.position, safeIndicatorRight);
        Gizmos.DrawWireSphere(transform.position, spawnRangeMin);
    }
}
