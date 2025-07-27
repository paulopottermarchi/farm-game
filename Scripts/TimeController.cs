using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Set the singleton instance to this TimeController
            DontDestroyOnLoad(gameObject); // Keep this object alive across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }

    }

    public float currentTime; // Current time in the game

    public float dayStart, dayEnd; // Start and end times of the day

    public float timeSpeed = .25f; // Speed at which time progresses

    private bool timeActive;

    public int currentDay = 1; // Current day in the game
    public string dayEndScene;

    void Start()
    {
        currentTime = dayStart; // Initialize current time to the start of the day

        timeActive = true; // Set timeActive to true to start the time progression
    }

    // Update is called once per frame
    void Update()
    {
        if (timeActive == true)
        {


            currentTime += Time.deltaTime * timeSpeed; // Increment current time by the time passed since last frame

            if (currentTime > dayEnd)
            {
                currentTime = dayEnd; // Clamp current time to the end of the day
                EndDay(); // Call EndDay to handle the end of the day logic
            }

            if (UIController.instance != null)
            {
                UIController.instance.UpdateTimeText(currentTime); // Update the UI with the current time
            }
        }
    }

    public void EndDay()
    {
        timeActive = false;

        currentDay++; // Increment the current day when the day ends

        GridInfo.instance.GrowCrop(); // Call the GrowCrop method to advance crop growth at the end of the day

        PlayerPrefs.SetString("Transition", "WakeUp"); // Set a player preference to indicate the transition to the next day

        //StartDay(); // Call StartDay to reset the time for the new day
        SceneManager.LoadScene(dayEndScene); // Load the scene for the end of the day
    }

    public void StartDay()
    {
        timeActive = true; // Set timeActive to true to start the time progression for the new day
        currentTime = dayStart; // Reset current time to the start of the day

        AudioManager.instance.PlaySFX(6); // Play the sound effect for starting a new day
    }
}
