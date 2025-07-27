using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CropDisplay : MonoBehaviour
{

    public CropController.CropType crop; // The type of crop to display

    public Image cropImages; // Reference to the UI Image for displaying the crop icon
    public TMP_Text cropAmountText; // Reference to the UI Text for displaying the amount of crops

    //Update TMP text with the amount of crops
    public void UpdateDisplay()
    {
        CropInfo info = CropController.instance.GetCropInfo(crop); // Get the CropInfo for the specified crop type

        cropImages.sprite = info.finalCrop; // Use the correct Sprite field here
        cropAmountText.text = "x" + info.cropAmount; // Update the text to show the amount of crops
    }
}
