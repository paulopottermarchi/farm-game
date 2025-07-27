using UnityEngine;

public class MainMenuFallingObject : MonoBehaviour
{
    public float minFallSpeed = 2f, maxFallSpeed = 5f, minRotSpeed = -360f, maxRotSpeed = 360f; // Minimum and maximum speeds for falling and rotation
    private float fallSpeed, rotSpeed; // Speed of falling and rotation
    private float rotValue; // Rotation value for the object

    public float destroyHeight = -6f; // Height at which the object will be destroyed

    void Start()
    {
        fallSpeed = Random.Range(minFallSpeed, maxFallSpeed); // Randomly set the fall speed within the specified range
        rotSpeed = Random.Range(minRotSpeed, maxRotSpeed); // Randomly set the
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime; // Move the object downwards

        rotValue += rotSpeed * Time.deltaTime; // Update the rotation value
        transform.rotation = Quaternion.Euler(0f, 0f, rotValue); // Apply the rotation to the object

        if(transform.position.y < destroyHeight)
        {
            Destroy(gameObject); // Destroy the object if it falls below the destroy height
        }
    }
}
