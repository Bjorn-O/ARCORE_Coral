using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform _targetCoral;
    private float _distance;

    [SerializeField] private float minimumDistance = 0.5f;
    [SerializeField] private float speed = 5f;
    private bool _shouldMove = true;

    private void Update()
    {
        if (!_shouldMove) return;
        Move();
    }

    public void Target(Transform target)
    {
        _targetCoral = target;
    }

    private void Move()
    {
        if (CalculateDistance() < minimumDistance)
        {
            DisableMove();
            return;
        }
        var step = speed * Time.deltaTime;
        transform.position =  Vector3.MoveTowards(transform.position, _targetCoral.position, step);
    }

    private float CalculateDistance()
    {
        return Vector3.Distance(transform.position, _targetCoral.position);
    }

    public void DisableMove()
    {
        _shouldMove = false;
    }
}
