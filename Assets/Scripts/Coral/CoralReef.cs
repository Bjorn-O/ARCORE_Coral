using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralReef : MonoBehaviour
{
    [Range(0, 100)] [SerializeField]
    private float _health;

    [SerializeField] private Material[] materials;

    public float health
    {
        get => _health;
        set
        {
            if ((_health + value)> 100)
            {
                _health = 100;
            }
            else
            {
                _health += value;
            }
        }
    }

    public MeshRenderer[] components;

    private static readonly int SaturationAmount = Shader.PropertyToID("_Saturation_Amount");


    private void Start()
    {
        health = 100;
    }

    private void Awake()
    {
        components = GetComponentsInChildren<MeshRenderer>();
    }

    private void Update()
    {
        HandleSaturation();
    }

    public void DamageCoral()
    {
        _health -= 1;
    }

    private void HandleSaturation()
    {
        foreach (var mesh in components)
        {
            mesh.material.SetFloat(SaturationAmount, _health / 100);
        }
    }
}
