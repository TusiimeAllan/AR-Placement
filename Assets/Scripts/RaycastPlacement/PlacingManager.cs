using UnityEngine;

public class PlacingManager : MonoBehaviour
{
    private PlaceIndicator placeIndicator;
    public GameObject objectToPlace;
    private GameObject placedObject;

    private void Start()
    {
        placeIndicator = FindObjectOfType<PlaceIndicator>();    
    }

    public void ClickToPlace() {
        if(placedObject == null) {
            placedObject = Instantiate(objectToPlace, placeIndicator.transform.position, objectToPlace.transform.rotation);
        } else {
            placedObject.transform.position = placeIndicator.transform.position;
        }
        
    }
}
