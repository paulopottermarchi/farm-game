using UnityEngine;

public class MainMenuBGObjectSpawner : MonoBehaviour
{
    public Transform minPos,maxPos; // Minimum and maximum positions for spawning objects

    public GameObject[] objects;

    public float TimeBetweenSpawns;
    private float spawnCounter;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter -= Time.deltaTime; // Decrease the spawn counter by the time since the last frame
        if (spawnCounter <= 0)
        {
            spawnCounter = TimeBetweenSpawns; // Reset the spawn counter

            GameObject newObject = Instantiate(objects[Random.Range(0, objects.Length)]);

            newObject.transform.position = new Vector3(Random.Range(minPos.position.x, maxPos.position.x), minPos.position.y, 0f); // Randomly set the x position within the defined range, y is set to minPos
            newObject.SetActive(true); // Activate the new object 
        }
    }
}
