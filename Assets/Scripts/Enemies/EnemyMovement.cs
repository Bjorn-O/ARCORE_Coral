using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform targetReef;
    private float distance;

    [SerializeField] private float minimumDistance = 0.5f;
    [SerializeField] private float speed = 5f;

    private void Awake()
    {
        targetReef = GameObject.FindObjectOfType<CoralReef>().transform;
    }

    private void Update()
    {
        Move();
    }

    public void Target(Transform target)
    {
        targetReef = target;
    }

    private void Move()
    {
        if (CalculateDistance() < minimumDistance) return;
        print("I'm moving");
        var step = speed * Time.deltaTime;
        transform.position =  Vector3.MoveTowards(transform.position, targetReef.position, step);
    }

    private float CalculateDistance()
    {
        return Vector3.Distance(transform.position, targetReef.position);
    }
}
