using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private float minimumDistance = 0.5f;
    [SerializeField] private float speed = 5f;
    
    private Vector3 _targetCoral;
    private float _distance;
    private bool _shouldMove = true;
    private bool _targetSet;

    private void Awake()
    {
        enemy.OnBroadcastLocation += Target;
        enemy.OnDetach += DisableMovement;
        enemy.ChangeState(EnemyState.Moving);
    }

    private void Update()
    {
        if (_shouldMove)
        {
            Move();
        }
    }

    public void Target(Vector3 target)
    { 
        _targetCoral = target;
        transform.LookAt(target);
        _targetSet = true;
    }

    private void Move()
    {
        var step = speed * Time.deltaTime;
        transform.position =  Vector3.MoveTowards(transform.position, _targetCoral, step);
        if (CalculateDistance() < minimumDistance)
        {
            _shouldMove = false;
            enemy.ChangeState(EnemyState.Attached);
        }
    }

    private float CalculateDistance()
    {
        return Vector3.Distance(transform.position, _targetCoral);
    }

    public void DisableMovement()
    {
        _shouldMove = false;
    }
}
