using UnityEngine;
using System.Collections.Generic;

public class CropController : MonoBehaviour
{
    public static CropController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object alive across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public enum CropType
    {
        pumpkin,
        lettuce,
        carrot,
        hay,
        potato,
        strawberry,
        tomato,
        avocado
    }

    public List<CropInfo> cropList = new List<CropInfo>();

    public CropInfo GetCropInfo(CropType cropToGet)
    {
        int position = -1;

        for (int i = 0; i < cropList.Count; i++)
        {
            if (cropList[i].cropType == cropToGet)
            {
                position = i; // Find the index of the crop type in the list
            }
        }

        if (position >= 0)
        {
            return cropList[position]; // Return the CropInfo if found
        }
        else
        {
            return null; // Return null if the crop type is not found
        }
    }

    public void UseSeed(CropType seedToUse)
    {
        foreach (CropInfo info in cropList)
        {
            if (info.cropType == seedToUse)
            {
                info.seedAmount--; // Decrease the seed amount for the specified crop type
            }
        }
    }

    public void AddCrop(CropType cropToAdd)
    {
        foreach (CropInfo info in cropList)
        {
            if (info.cropType == cropToAdd)
            {
                info.cropAmount++; // Increase the crop amount for the specified crop type 9
            }
        }
    }

    public void AddSeed(CropType seedToAdd, int amount)
    {
        foreach (CropInfo info in cropList)
        {
            if (info.cropType == seedToAdd)
            {
                info.seedAmount += amount; // Increase the seed amount for the specified crop type
            }
        }
    }

    public void RemoveCrop(CropType cropToRemove)
    {
        foreach (CropInfo info in cropList)
        {
            if (info.cropType == cropToRemove)
            {
                info.cropAmount = 0; // Increase the seed amount for the specified crop type
            }
        }
    }
}

[System.Serializable]
public class CropInfo
{
    public CropController.CropType cropType; // Type of the crop
    public Sprite finalCrop, seedType, planted, growStage1, growStage2, ripe;

    public int seedAmount, cropAmount; // Amount of seeds and crops available

    [Range(0f, 100f)]
    public float growthFailChance;

    public float seedPrice, cropPrice; // Prices for seeds and crops
}
