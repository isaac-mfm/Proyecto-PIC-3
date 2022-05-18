using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

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
        foreach (Touch touch in Input.touches)
        {
            int id = touch.fingerId;
            if (EventSystem.current.IsPointerOverGameObject(id))
            {
                return;
            }
        }
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
            else if (Input.GetTouch(0).phase != TouchPhase.Began)
            {
                currentFurniture.transform.position = hitPose.position;
            }
        }
    }
    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }
    public void Place()
    {
        if (currentFurniture)
        {
            Debug.Log("Se coloco el objeto: " + currentFurniture.name);
            currentFurniture = null;
            FurniturePlacement.INSTANCE.placementMenu.style.display = UnityEngine.UIElements.DisplayStyle.None;
        }
    }

    public void Cancel()
    {
        if (currentFurniture) 
        {
            Debug.Log("Se cancelo la colocacion del objeto: " + currentFurniture.name);
            DestroyImmediate(currentFurniture.gameObject);
            currentFurniture = null;
            FurniturePlacement.INSTANCE.placementMenu.style.display = UnityEngine.UIElements.DisplayStyle.None;
        }
    }
    public void Rotate(float angle)
    {
        currentFurniture.transform.Rotate(new Vector3(0, angle, 0), Space.World);
    }
}
