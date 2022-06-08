using UnityEngine;

namespace Input
{
    public class CurrentTouch
    {
        private GameObject _touchedObject;
        private Transform _startPosition;
        private Draggable _draggable;

        private Vector2 _dragDirection;
        private float _dragDistance;
        
        public CurrentTouch(Draggable draggable, GameObject touchedObject, Transform position)
        {
            _draggable = draggable;
            _touchedObject = touchedObject;
            _startPosition = position;
            Debug.Log("I'm alive");
        }

        public void Update(Vector2 direction)
        {
            _draggable.AddTension(direction);
        }

        public void Released()
        {
            Debug.Log("dying");
            _draggable.onTensionRelease.Invoke();
        }
    }
}