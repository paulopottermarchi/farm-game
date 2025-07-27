using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform target;

    public Transform clampMin, clampMax;

    private Camera cam;
    private float halfWidth, halfHeight;

    /*Start is called once before the first execution of Update after the MonoBehaviour is created
    * This method initializes the camera controller by finding the player object
    * and setting the camera's orthographic size and aspect ratio for clamping purposes
    * It also sets the parent of clampMin and clampMax to null to avoid unwanted transformations
    * It is called once when the script instance is being loaded
    * It sets the target to the player's transform and calculates the half width and height of the camera
    * It also detaches clampMin and clampMax from their parents to ensure they are not affected by the camera's transformations
    * It is called once when the script instance is being loaded
    It sets the camera's orthographic size and aspect ratio for clamping purposes */

    void Start()
    {
        //target = FindAnyObjectByType<PlayerController>().transform;
        target = PlayerController.instance.transform; // Get the player's transform from the PlayerController singleton instance

        clampMin.SetParent(null);
        clampMax.SetParent(null);

        cam = GetComponent<Camera>();
        halfHeight = cam.orthographicSize;
        halfWidth = cam.orthographicSize * cam.aspect;
    }

    /* Update is called once per frame
    *  This method updates the camera position to follow the target (player)
    *  and clamps the camera position within specified bounds */


    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        Vector3 clampedPosition = transform.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, clampMin.position.x + halfWidth, clampMax.position.x - halfWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight);

        transform.position = clampedPosition;
    }
}
