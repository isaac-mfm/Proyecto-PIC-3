using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ARTapToPlace))]
public class ARTouchRotation : MonoBehaviour
{
    private ARTapToPlace arTapToPlace;
    private void Awake()
    {
        arTapToPlace = GetComponent<ARTapToPlace>();
    }
    private void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            switch (touch0.phase) {
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Moved:
                    break;
                default: 
                    return;
            }

            Touch touch1 = Input.GetTouch(1);
            switch (touch1.phase) {
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Moved:
                    break;
                default:
                    return;
            }

            var pos1 = touch0.position;
            var pos2 = touch1.position;
            var pos1b = touch0.position - touch0.deltaPosition;
            var pos2b = touch1.position - touch1.deltaPosition;
            arTapToPlace.Rotate(Vector3.SignedAngle(pos2b - pos1b, pos2 - pos1, Vector3.forward));
        }
    }
}
