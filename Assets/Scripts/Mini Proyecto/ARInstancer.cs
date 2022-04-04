using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARInstancer : MonoBehaviour
{
    public static ARInstancer INSTANCE = null;
    public GameObject prefab;
    private GameObject spawnedObject;
    private ARRaycastManager arRaycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Vector2 touchPosition;
    private void Awake()
    {
        INSTANCE = this;
        arRaycastManager = GetComponent<ARRaycastManager>();
    }
    private void Update()
    {
        if (!TryGetTouchPosition(out touchPosition)) return;
        if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            if (!spawnedObject)
            {
                spawnedObject = Instantiate(prefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnedObject.transform.position = hitPose.position;
            }
        }
    }
    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(0))
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }
}
