using UnityEngine;
using UnityEngine.InputSystem; // Adicione esta linha

public class GrowBlock : MonoBehaviour
{

    public enum GrowthStage
    {
        barren,
        ploughed,
        planted,
        growing1,
        growing2,
        ripe
    }

    public GrowthStage currentStage;

    public SpriteRenderer theSR;
    public Sprite soilTilled, soilWatered;

    public SpriteRenderer cropSR;
    public Sprite cropPlanted, cropGrowing1, cropGrowing2, cropRipe;

    public bool isWatered;

    public bool preventUse;

    private Vector2Int gridPosition;

    public CropController.CropType cropType;
    public float growFailChance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /* if (Keyboard.current.eKey.wasPressedThisFrame)
         {
             AdvanceStage();
             SetSoilSprite();
         }
   */
#if UNITY_EDITOR
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            AdvanceCrop();
        }
#endif


    }

    // This method advances the growth stage of the soil
    // It cycles through the stages: barren -> ploughed -> planted -> growing1 -> growing2 -> ripe
    // If the current stage is ripe, it resets to barren


    void AdvanceStage()
    {
        currentStage++;

        if ((int)currentStage >= 6)
        {
            // Reset to barren stage if ripe
            currentStage = GrowthStage.barren;
        }
    }

    // This method sets the soil sprite based on the current growth stage
    // If the stage is barren, it sets the sprite to null
    public void SetSoilSprite()
    {
        if (currentStage == GrowthStage.barren)
        {
            theSR.sprite = null; // Reset sprite for barren stage
        }
        else
        {
            if (isWatered == true)
            {
                theSR.sprite = soilWatered;
            }
            else
            {
                theSR.sprite = soilTilled;
            }

        }

        UpdateGridInfo(); // Update the grid information after changing the soil sprite
    }

    // This method is called when the player uses a tool to plough the soil
    // It changes the growth stage from barren to ploughed
    // and updates the soil sprite accordingly
    public void PloughSoil()
    {
        if (currentStage == GrowthStage.barren && isWatered == false && preventUse == false)
        {
            currentStage = GrowthStage.ploughed;
            SetSoilSprite();

            AudioManager.instance.PlaySFXPitchAdjusted(4); // Play the ploughing sound effect
        }
    }

    public void WaterSoil()
    {
        isWatered = true;

        SetSoilSprite();

        AudioManager.instance.PlaySFXPitchAdjusted(7);
    }

    public void PlantCrop(CropController.CropType cropToPlant)
    {
        if (currentStage == GrowthStage.ploughed && isWatered == true)
        {
            currentStage = GrowthStage.planted;

            cropType = cropToPlant; // Set the crop type being planted

            growFailChance = CropController.instance.GetCropInfo(cropType).growthFailChance; // Get the growth failure chance from CropController

            CropController.instance.UseSeed(cropToPlant); // Use the seed from the CropController

            UpdateCropSprite();

            AudioManager.instance.PlaySFXPitchAdjusted(3); // Play the planting sound effect
        }
    }

    public void UpdateCropSprite()
    {
        CropInfo activeCrop = CropController.instance.GetCropInfo(cropType);

        switch (currentStage)
        {
            case GrowthStage.planted:
                //cropSR.sprite = cropPlanted;
                cropSR.sprite = activeCrop.planted; // Use the sprite from CropInfo
                break;
            case GrowthStage.growing1:
                //cropSR.sprite = cropGrowing1;
                 cropSR.sprite = activeCrop.growStage1;
                break;
            case GrowthStage.growing2:
                //cropSR.sprite = cropGrowing2;
                 cropSR.sprite = activeCrop.growStage2;
                break;
            case GrowthStage.ripe:
                //cropSR.sprite = cropRipe;
                 cropSR.sprite = activeCrop.ripe;
                break;
        }
        UpdateGridInfo(); // Update the grid information after changing the crop sprite
    }

    public void AdvanceCrop()
    {
        if (isWatered == true && preventUse == false)
        {
            if (currentStage == GrowthStage.planted || currentStage == GrowthStage.growing1 || currentStage == GrowthStage.growing2)
            {
                currentStage++;

                isWatered = false; // Reset watering after advancing crop stage
                SetSoilSprite();
                // Update the crop sprite based on the new growth stage
                UpdateCropSprite();
            }
        }

    }

    public void HarvestCrop()
    {
        if (currentStage == GrowthStage.ripe && preventUse == false)
        {
            currentStage = GrowthStage.ploughed; // Reset to ploughed stage after harvesting
            SetSoilSprite();
            cropSR.sprite = null; // Reset crop sprite after harvesting
            CropController.instance.AddCrop(cropType); // Add the harvested crop to the CropController

             AudioManager.instance.PlaySFXPitchAdjusted(2); // Play the harvest sound effect
        }
    }

    public void SetGridPosition(int x, int y)
    {
        gridPosition = new Vector2Int(x, y);
    }

    void UpdateGridInfo()
    {
        GridInfo.instance.UpdateInfo(this, gridPosition.x, gridPosition.y);
    }
}
