using Input;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject enemyToSpawn;
    
    [Header("Spawn Timer")]
    [SerializeField] private float initialDelay;
    [SerializeField] private float spawnDelay;

    [Header("SpawnDirection")] 
    [Range(0, 360)]
    [SerializeField] private float spawnDirection;
    [Range(0, 360)]
    [SerializeField] private float spawnAngle;
    [SerializeField] private float spawnRangeMax;
    [SerializeField] private float spawnRangeMin;

    [Header("TestValues")] 
    [SerializeField] private bool testBool;

    [Header("GizmoSettings")] 
    [SerializeField] private bool drawGizmos;
    [Range(0,360)]
    [SerializeField] private int edgeIndicators;
    [Range(0.05f, 1f)]
    [SerializeField] private float edgeIndicatorSize = 0.1f;
    [SerializeField] private Color edgeIndicatorColor;
    
    private Transform _myTransform;
    
    private void Awake()
    {
        this._myTransform = transform;
    }

    private void Update()
    {
        if (testBool)
        {
            InitiateEnemy();
            testBool = false;
        }
    }

    private void InitiateEnemy()
    {
        var enemy = Instantiate(enemyToSpawn, GetRandomSpawnPoint(), Quaternion.identity);
        enemy.transform.LookAt(_myTransform);
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
            Gizmos.color = edgeIndicatorColor;
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
