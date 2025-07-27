using UnityEngine;

public class InventoryController : MonoBehaviour
{

    public SeedDisplay[] seeds;
    public CropDisplay[] crops;

    public void OpenClose()
    {
        if (UIController.instance.theShop.gameObject.activeSelf == false)
        {
        
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true); // Activate the inventory UI if it is not active

            UpdateDisplay(); // Update the display of seeds when the inventory is opened
        }
        else
        {
            gameObject.SetActive(false); // Deactivate the inventory UI if it is currently active
        }
        }
    }

    public void UpdateDisplay()
    {
        foreach (SeedDisplay seed in seeds)
        {
            seed.UpdateDisplay(); // Update the display for each seed in the inventory
        }

        foreach (CropDisplay crop in crops)
        {
            crop.UpdateDisplay(); // Update the display for each crop in the inventory 
        }
    }
}
