using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopCropDisplay : MonoBehaviour
{
    public CropController.CropType crop; // The type of crop to display

    public Image cropImages; // Reference to the UI Image for displaying the crop icon
    public TMP_Text cropAmountText, priceText; // Reference to the UI Text for displaying the amount of crops

    //Update TMP text with the amount of crops
    public void UpdateDisplay()
    {
        CropInfo info = CropController.instance.GetCropInfo(crop); // Get the CropInfo for the specified crop type

        cropImages.sprite = info.finalCrop; // Use the correct Sprite field here
        cropAmountText.text = "x" + info.cropAmount; // Update the text to show the amount of crops

        priceText.text = "$" + info.cropPrice + " each "; // Update the text to show the price of the crop
    }

    public void SellCrop()
    {
        CropInfo info = CropController.instance.GetCropInfo(crop);

        if (info.cropAmount > 0)
        {
            CurrencyController.instance.AddMoney(info.cropAmount * info.cropPrice); // Add money based on the amount of crops sold

            CropController.instance.RemoveCrop(crop); // Remove the crop from the player's inventory

            UpdateDisplay(); // Update the display after selling the crop
            
            AudioManager.instance.PlaySFXPitchAdjusted(5);
        }

    }
}
