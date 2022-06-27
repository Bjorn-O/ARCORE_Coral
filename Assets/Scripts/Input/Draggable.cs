using UnityEngine;
using UnityEngine.Events;

namespace Input
{
    [RequireComponent(typeof(Rigidbody))]
    public class Draggable : MonoBehaviour
    {
        [Header("Components")] [SerializeField]
        private Rigidbody rb;

        [Header("Variables")] 
        [SerializeField] private float tensileLimit;
        [SerializeField] private float flingSpeed;

        [Header("Events")] public UnityEvent onTensionOverload;
        public UnityEvent onTensionRelease;
        public UnityEvent onTensionIncrease;
        public UnityEvent onFlung;

        private Vector2 _tensileDirection;
        private float _tensileStrength;
        
        

        private void Awake()
        {
            rb.isKinematic = true;
            Debug.Log(this.gameObject.name + ": I'm grabbed!");
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
            Debug.Log("I'M FREE!");
        }

        public void CheckTension()
        {
            if (_tensileStrength > tensileLimit)
            {
                Debug.Log("FWOOSH!");
                Fling(_tensileDirection, _tensileStrength);
                onTensionOverload?.Invoke();
            }
        }

        private void Fling(Vector3 direction, float speed)
        {
            rb.isKinematic = false;
            rb.AddForce(direction * flingSpeed);
            var x = Random.Range(0, 1f);
            var y = Random.Range(0f, 1f);
            var z = Random.Range(0f, 1f);
            rb.AddTorque(new Vector3(x,y,z) * 75);
            onFlung?.Invoke();
        }
    }
}