using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARRaycastPlace : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject objectToPlace;
    private GameObject placedObject;
    public Camera arCamera;

    private List<ARRaycastHit> hits = new();

    private void Update()
    {
        // Checking if there is touch input
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            TouchState touch = Touchscreen.current.primaryTouch.ReadValue();

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);

                if (raycastManager.Raycast(ray, hits, TrackableType.Planes))
                {
                    Pose hitPose = hits[0].pose;
                    if(placedObject == null) {
                        placedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
                    } else {
                        placedObject.transform.position = hitPose.position;
                        placedObject.transform.rotation = hitPose.rotation;
                    }
                    
                }
            }
        }
    }
}
