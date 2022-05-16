using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlace : MonoBehaviour
{
    public static ARTapToPlace INSTANCE = null;
    public Furniture selectedFurniture;
    private Furniture currentFurniture;
    private List<Furniture> spawnedFurnitures;
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
            if (!currentFurniture)
            {
                var spawnedFurniture = Instantiate(selectedFurniture, hitPose.position, hitPose.rotation);
                currentFurniture = spawnedFurniture;
                spawnedFurnitures.Add(spawnedFurniture);
            }
            else
            {
                currentFurniture.transform.position = hitPose.position;
            }
        }
    }
    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }
    public void PlaceFurniture()
    {
        currentFurniture = null;
    }

    public void Cancel()
    {
        if (currentFurniture) Destroy(currentFurniture.gameObject);
        currentFurniture = null;
    }
}
