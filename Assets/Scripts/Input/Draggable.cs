using System;
using UnityEngine;
using UnityEngine.Events;

namespace Input
{
    [RequireComponent(typeof(Rigidbody))]
    public class Draggable : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private Rigidbody rb;
        
        [Header("Variables")]
        [SerializeField] private float tensileLimit;
        
        [Header("Events")]
        public UnityEvent onTensionOverload;
        public UnityEvent onTensionRelease;
        public UnityEvent onTensionIncrease;
        
        private Vector2 _tensileDirection;
        private float _tensileStrength;

        private void Start()
        {
            
        }

        public void AddTension(Vector2 direction)
        {
            _tensileDirection += direction.normalized;
            _tensileStrength += direction.magnitude * Time.deltaTime;
            Debug.Log(_tensileStrength + " . " + _tensileDirection);
            onTensionIncrease?.Invoke();
        }

        public void ResetTension()
        {
            _tensileDirection -= _tensileDirection;
            _tensileStrength = 0;
        }

        public void CheckTension()
        {
            if (_tensileStrength > tensileLimit)
            {
                Debug.Log("FWOOSH!");
                Fling(_tensileDirection,_tensileStrength);
                onTensionOverload?.Invoke();
            }
        }
        
        private void Fling(Vector3 direction, float speed)
        {
            rb.AddForce(direction * speed);
        }
    }
}