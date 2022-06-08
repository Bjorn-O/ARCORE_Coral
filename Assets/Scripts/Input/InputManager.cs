using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Input
{
    [DefaultExecutionOrder(-1)]
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        
        public delegate void StartTouchEvent(Vector2 pos, float time);
        public event StartTouchEvent OnStartTouch;
        public delegate void EndTouchEvent(Vector2 pos, float time);
        public event EndTouchEvent OnEndTouch;
        
        private CurrentTouch _currentTouch;
        private Controls _controls;

        protected void  Awake()
        {
            _controls = new Controls();
        }

        private void OnEnable()
        {
            _controls.Enable();
            _controls.Touch.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
            _controls.Touch.Enable();
        }

        private void Start()
        {
            _controls.Touch.TouchPress.started += ctx => StartTouch(ctx);
            _controls.Touch.TouchLifted.canceled += ctx => EndTouch(ctx);

        }

        private void Update()
        {
            UpdateTouch(_currentTouch);
        }

        private void UpdateTouch(CurrentTouch touch)
        {
            if(touch == null) return;
            Debug.Log("Dragging");
            touch.Update(_controls.Touch.TouchDelta.ReadValue<Vector2>());
        }

        private void StartTouch(InputAction.CallbackContext context)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(_controls.Touch.TouchPosition.ReadValue<Vector2>());
            if (Physics.Raycast(ray, out hit))
            {
                var draggable = hit.collider.gameObject.GetComponent<Draggable>();
                if (draggable == null) return;
                _currentTouch = new CurrentTouch(draggable, hit.collider.gameObject, hit.transform);
            }
        }
        
        private void EndTouch(InputAction.CallbackContext context)
        {
            _currentTouch.Released();
            _currentTouch = null;
        }
        
    }
}