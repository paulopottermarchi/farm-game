using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSeedDisplay : MonoBehaviour
{
    public CropController.CropType crop;
    public Image seedImage;
    public TMP_Text seedAmount, priceText;


    public void UpdateDisplay()
    {
        CropInfo info = CropController.instance.GetCropInfo(crop); // Get the CropInfo for the specified crop type

        seedImage.sprite = info.seedType; // Set the seed icon image from CropInfo
        seedAmount.text = "x" + info.seedAmount; 
        
        priceText.text = "$" + info.seedPrice + " each"; // Display the price of the seed
    }

    public void BuySeed(int amout)
    {
        CropInfo info = CropController.instance.GetCropInfo(crop);

        if (CurrencyController.instance.CheckMoney(info.seedPrice * amout))
        {
            CropController.instance.AddSeed(crop, amout); // Add seeds to the player's inventory

            CurrencyController.instance.SpendMoney(info.seedPrice * amout); // Deduct the cost from the player's currency

            UpdateDisplay(); // Update the display to reflect the new amount of seeds
            
            AudioManager.instance.PlaySFXPitchAdjusted(5);
        }
    }
}
