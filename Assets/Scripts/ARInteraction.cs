using UnityEngine;

public class ARInteraction : MonoBehaviour
{
    private float initialDistance;
    private Vector3 initialScale;

    private void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            float currentDistance = Vector2.Distance(touch1.position, touch2.position);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                initialDistance = currentDistance;
                initialScale = transform.localScale;
            }
            else
            {
                float scaleFactor = currentDistance / initialDistance;
                transform.localScale = initialScale * scaleFactor;
            }
        }
    }
}
