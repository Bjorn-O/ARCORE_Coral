using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralHealthManager : MonoBehaviour
{ 
    public MeshRenderer[] components;

    private int _enemyCount;
    private float _health;
    private static readonly int SaturationAmount = Shader.PropertyToID("_Saturation_Amount");

    private void Start()
    {
        _health = 35;
    }

    private void Awake()
    {
        components = GetComponentsInChildren<MeshRenderer>();
    }

    private void Update()
    {
        HandleSaturation();
        UpdateHealth(_enemyCount);
    }

    private void UpdateHealth(int i)
    {
        if (i > 0) _health -= i * Time.deltaTime * 3;
        else
        {
            _health += 1 * Time.deltaTime * 5;
        }

        switch (_health)
        {
            case < 0:
                _health = 0;
                break;
            case > 100:
                _health = 100;
                break;
        }
    }

    public void AddEnemy()
    {
        _enemyCount++;
    }

    public void RemoveEnemy()
    {
        _enemyCount--;
        if (_enemyCount < 0)
        {
            _enemyCount = 0;
        }
    }
    
    private void HandleSaturation()
    {
        foreach (var mesh in components)
        {
            mesh.material.SetFloat(SaturationAmount, _health / 100);
        }
    }
}
