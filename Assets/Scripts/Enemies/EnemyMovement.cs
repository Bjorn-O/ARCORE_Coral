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
    private bool shouldMove = true;

    private void Awake()
    {
        targetReef = GameObject.FindObjectOfType<CoralReef>().transform;
    }

    private void Update()
    {
        if (!shouldMove) return;
        Move();
    }

    public void Target(Transform target)
    {
        targetReef = target;
    }

    private void Move()
    {
        if (CalculateDistance() < minimumDistance)
        {
            DisableMove();
            return;
        }
        print("I'm moving");
        var step = speed * Time.deltaTime;
        transform.position =  Vector3.MoveTowards(transform.position, targetReef.position, step);
    }

    private float CalculateDistance()
    {
        return Vector3.Distance(transform.position, targetReef.position);
    }

    public void DisableMove()
    {
        shouldMove = false;
    }
}
