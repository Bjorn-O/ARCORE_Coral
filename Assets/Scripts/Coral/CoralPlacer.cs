using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;


public class CoralPlacer : MonoBehaviour
{
    [SerializeField] private Transform raycastPos;
    [SerializeField] private LayerMask layerMask;
    private ARRaycastManager _raycastManager;
    private bool _grounded;

    public UnityEvent onGrounded = new UnityEvent();
    
    private void Awake()
    {
        _raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    private void Update()
    {
        if (_raycastManager != null && !_grounded)
        {
            FindGround();
        }
    }

    private void FindGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastPos.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            transform.position = hit.transform.position;
            _grounded = true;
            onGrounded.Invoke();
        }
    }
}
