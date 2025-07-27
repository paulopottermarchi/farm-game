using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string levelToStart; // The name of the level to load when starting the game

    private void Start()
    {
        AudioManager.instance.PlayTitle(); // Play the title music when the main menu starts
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(levelToStart); // Load the specified level when the game starts

        AudioManager.instance.PlayNextBGM(); // Start playing the next background music track

        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the application when the quit button is pressed

        Debug.Log("Game is quitting..."); // Log a message to the console for debugging purposes

        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }

    
}
