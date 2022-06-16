using UnityEngine;

namespace Input
{
    public class CurrentTouch
    {
        private GameObject _touchedObject;
        private Transform _startPosition;
        private Draggable _draggable;

        private bool isDraggable = false;
        private Vector2 _dragDirection;
        private float _dragDistance;
        
        public CurrentTouch(Draggable draggable, GameObject touchedObject, Transform position)
        {
            if (draggable != null)
            {
                _draggable = draggable;
                isDraggable = true;
            }
            _touchedObject = touchedObject;
            _startPosition = position;
            Debug.Log("I'm alive");
        }

        public void Update(Vector2 direction)
        {
            if (!isDraggable) return;
            _draggable.AddTension(direction);
        }

        public void Released()
        {
            if (!isDraggable) return;
            _draggable.onTensionRelease.Invoke();
        }
    }
}