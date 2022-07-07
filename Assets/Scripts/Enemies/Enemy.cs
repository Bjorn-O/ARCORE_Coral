using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Action OnAttach;
    public Action OnDetach;
    public Action<Vector3> OnBroadcastLocation;

    [SerializeField] private GameObject[] miningParticles;
    
    private EnemyState _currentState;
    private Vector3 _target;

    private void Awake()
    {
        SetParticlesActive(false);
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
        OnBroadcastLocation?.Invoke(_target);
    }

    public void ChangeState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Moving:
                _currentState = EnemyState.Moving;
                break;
            case EnemyState.Attached:
                _currentState = EnemyState.Attached;
                SetParticlesActive(true);
                OnAttach.Invoke();
                break;
            case EnemyState.Flung:
                if (_currentState == EnemyState.Attached)
                {
                    OnDetach.Invoke();
                    SetParticlesActive(false);
                }
                _currentState = EnemyState.Flung;
                break;
            default:
                return;
        }
    }

    private void SetParticlesActive(bool b)
    {
        foreach (var particle in miningParticles)
        {
            particle.SetActive(b);
        }
    }
}

public enum EnemyState
{
    Moving,
    Attached,
    Flung
}
