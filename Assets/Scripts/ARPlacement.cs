using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    public static ARPlacement Instance;
    public GameObject AR_Model;
    [SerializeField] private ARRaycastManager arRaycastManager;
    private GameObject placedModel;
    [SerializeField] private Vector2 touchPosition;

    private void Awake()
    {
        if(Instance == null) {
            Instance = this;
        }   
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceModel();
            }
        }
    }
    
    private void InitializeARRaycastManager() {
        arRaycastManager.raycastPrefab = AR_Model;
    }

    private void PlaceModel()
    {
        if(AR_Model != null) {
            InitializeARRaycastManager();
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                if (placedModel == null)
                {
                    placedModel = Instantiate(AR_Model, hitPose.position, hitPose.rotation);
                }
                else
                {
                    placedModel.transform.position = hitPose.position;
                }
            }
        }
    }
}
