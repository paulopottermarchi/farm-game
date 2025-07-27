using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public static UIController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensure the UIController persists across scene loads
        }
        else
        {
            Destroy(gameObject); // If another instance exists, destroy this one to maintain singleton pattern
        }
    }
    public GameObject[] toolbarActivatorIcons;

    public TMP_Text timeText; // Reference to the UI text element for displaying time

    public InventoryController theIC;
    public ShopController theShop;

    public Image seedImage; // Reference to the UI Image component for displaying the seed icon

    public TMP_Text moneyText; // Reference to the UI text element for displaying the player's money

    public GameObject pauseScreen; // Reference to the pause screen UI element
    public string mainMenuScene; // Name of the main menu scene to load
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            theIC.OpenClose(); // Toggle the inventory when the 'I' key is pressed
        }

#if UNITY_EDITOR
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            theShop.OpenClose(); // Toggle the shop when the 'B' key is pressed
        }
#endif

        if (Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.pKey.wasPressedThisFrame)
        {
            PauseUnpause(); // Toggle the pause screen when the 'Escape' or 'P' key is pressed
        }
        
    }

    public void SwitchTool(int selected)
    {
        foreach (GameObject icon in toolbarActivatorIcons)
        {
            icon.SetActive(false);
        }

        toolbarActivatorIcons[selected].SetActive(true);
    }

    public void UpdateTimeText(float currentTime)
    {
        if (currentTime < 12)
        {
            timeText.text = Mathf.FloorToInt(currentTime) + " AM"; // Display time in AM format
        }
        else if (currentTime < 13)
        {
            timeText.text = "12PM";
        }
        else if (currentTime < 24)
        {
            timeText.text = Mathf.FloorToInt(currentTime - 12) + " PM";
        }
        else if (currentTime < 25)
        {
            timeText.text = "12AM";
        }
        else
        {
            timeText.text = Mathf.FloorToInt(currentTime - 24) + " AM"; // Display time in AM format for times after midnight
        }
    }

    public void SwitchSeed(CropController.CropType crop)
    {
        seedImage.sprite = CropController.instance.GetCropInfo(crop).seedType; // Update the seed icon in the UI

        AudioManager.instance.PlaySFXPitchAdjusted(5);

    }

    public void UpdateMoneyText(float currentMoney)
    {
        moneyText.text = "$" + currentMoney;
    }

    public void PauseUnpause()
    {
        if (pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true); // Show the pause screen

            Time.timeScale = 0f; // Pause the game by setting time scale to 0
        }
        else
        {
            pauseScreen.SetActive(false); // Hide the pause screen

            Time.timeScale = 1f; // Resume the game by setting time scale back to 1
        }

        AudioManager.instance.PlaySFXPitchAdjusted(5); // Play the pause sound effect
    }

    public void MainMenu()
    {
        Time.timeScale = 1f; // Ensure the game is unpaused when returning to the main menu
        SceneManager.LoadScene(mainMenuScene); // Load the main menu scene when called

        Destroy(gameObject); // Destroy the UIController instance to prevent it from persisting in the main menu
        Destroy(PlayerController.instance.gameObject); // Destroy the PlayerController instance to reset the game state
        Destroy(GridInfo.instance.gameObject); // Destroy the GridController instance to reset the grid state
        Destroy(TimeController.instance.gameObject); // Destroy the TimeController instance to reset time state
        Destroy(CropController.instance.gameObject); // Destroy the CropController instance to reset crop data
        Destroy(CurrencyController.instance.gameObject); // Destroy the CurrencyController instance to reset currency data

        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the game when called

        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }
}
