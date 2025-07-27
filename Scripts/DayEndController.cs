using UnityEngine;
using TMPro; // Added TMPRO namespace
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DayEndController : MonoBehaviour
{
    public TMP_Text dayText; // Reference to the UI text element for displaying the day number

    public string wakeUpScene; // Scene to load when the day ends
    void Start()
    {
        if (TimeController.instance != null)
        {
            dayText.text = "- Day " + TimeController.instance.currentDay + " -"; // Initialize the day text with the current day number
        }

        AudioManager.instance.PauseMusic(); // Pause the music when the day ends

         AudioManager.instance.PlaySFX(1); // Play the day end sound effect
    }

    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            TimeController.instance.StartDay(); // Call StartDay to reset the time and load the wake-up scene

            AudioManager.instance.ResumeMusic(); // Resume the music when the day starts

            SceneManager.LoadScene(wakeUpScene); // Load the scene for the new day
        }
    }

}
