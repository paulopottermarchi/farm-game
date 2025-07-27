using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeedDisplay : MonoBehaviour
{
    public CropController.CropType crop;

    public Image seedImage; // Reference to the UI Image component for displaying the seed icon
    public TMP_Text seedAmount;

    public void UpdateDisplay()
    {
        CropInfo info = CropController.instance.GetCropInfo(crop); // Get the CropInfo for the specified crop type

        seedImage.sprite = info.seedType; // Set the seed icon image from CropInfo
        seedAmount.text = "x" + info.seedAmount;
    }

    public void SelectSeed()
    {
        PlayerController.instance.SwitchSeed(crop); // Switch the player's selected seed to the specified crop type

        UIController.instance.SwitchSeed(crop); // Update the UI to reflect the selected seed

        UIController.instance.theIC.OpenClose(); // Close the inventory UI after selecting a seed
    }
}
