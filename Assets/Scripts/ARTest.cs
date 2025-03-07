using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARTest : MonoBehaviour
{
    public GameObject MyObject;
    public ARRaycastManager RaycastManager;

    private void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            List<ARRaycastHit> touches = new();

            RaycastManager.Raycast(Input.GetTouch(0).position, touches, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

            if(touches.Count > 0) {
                Instantiate(MyObject, touches[0].pose.position, touches[0].pose.rotation);
            }
        }   
    }
}
