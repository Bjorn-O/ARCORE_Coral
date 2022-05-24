  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  using UnityEngine.XR.ARFoundation;

  public class ARCursor : MonoBehaviour
{
    public GameObject cursorChildObject;
    public GameObject objectToPlace;
    public ARRaycastManager raycastManager;

    public bool useCursror = true;
    
    // Start is called before the first frame update
    void Start()
    {
        cursorChildObject.SetActive(useCursror);
    }

    // Update is called once per frame
    private void Update()
    {
        if (useCursror)
        {
            UpdateCursor();
        }

        if (useCursror && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!useCursror) return;
            var transform1 = transform;
            GameObject.Instantiate(objectToPlace, transform1.position, transform1.rotation);
        }
        else
        {
            if (useCursror) return;
            var hits = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
            if (hits.Count > 0)
            {
                GameObject.Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
            }
        }
    }

    private void UpdateCursor()
    {
        Debug.Log("camera brrr");
        if (Camera.main is null) return;
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
        
        Debug.Log("Hits");
        if (hits.Count <= 0) return;
        transform.position = hits[0].pose.position;
        transform.rotation = hits[0].pose.rotation;
        Debug.Log(hits[0].pose.position);
    }
}
