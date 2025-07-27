using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GridInfo : MonoBehaviour
{

    public static GridInfo instance;

    

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

    public bool hasGrid;
    public List<InfoRow> theGrid;

    public void CreateGrid()
    {
        hasGrid = true; // Set hasGrid to true when the grid is created

        for (int y = 0; y < GridController.instance.blockRows.Count; y++)
        {
            theGrid.Add(new InfoRow()); // Add a new InfoRow for each row in the grid

            for (int x = 0; x < GridController.instance.blockRows[y].blocks.Count; x++)
            {
                theGrid[y].blocks.Add(new BlockInfo()); // Add a new BlockInfo for each block in the row
            }

        }
    }

    public void UpdateInfo(GrowBlock theBlock, int xPos, int yPos)
    {
        theGrid[yPos].blocks[xPos].currentStage = theBlock.currentStage; // Update the current stage of the block
        theGrid[yPos].blocks[xPos].isWatered = theBlock.isWatered; // Update whether the block is watered
        theGrid[yPos].blocks[xPos].cropType = theBlock.cropType; // Update the crop type of the block
        theGrid[yPos].blocks[xPos].growFailChance = theBlock.growFailChance; // Update the growth failure chance of the block
    }

    public void GrowCrop()
    {
        for (int y = 0; y < theGrid.Count; y++)
        {
            for (int x = 0; x < theGrid[y].blocks.Count; x++)
            {
                if (theGrid[y].blocks[x].isWatered == true)
                {
                    float growthFailTest = Random.Range(0f, 100f); // Generate a random number between 0 and 100

                    if (growthFailTest > theGrid[y].blocks[x].growFailChance)
                    {
                        switch (theGrid[y].blocks[x].currentStage)
                        {
                            case GrowBlock.GrowthStage.planted:
                                theGrid[y].blocks[x].currentStage = GrowBlock.GrowthStage.growing1; // Advance to growing1 stage
                                break;

                            case GrowBlock.GrowthStage.growing1:
                                theGrid[y].blocks[x].currentStage = GrowBlock.GrowthStage.growing2; // Advance to growing2 stage
                                break;

                            case GrowBlock.GrowthStage.growing2:
                                theGrid[y].blocks[x].currentStage = GrowBlock.GrowthStage.ripe; // Advance to ripe stage
                                break;
                        }
                    }
                    theGrid[y].blocks[x].isWatered = false; // Reset watering after advancing crop stage
                }
                
                if(theGrid[y].blocks[x].currentStage == GrowBlock.GrowthStage.ploughed)
                {
                    theGrid[y].blocks[x].currentStage = GrowBlock.GrowthStage.barren; // Reset to barren if the crop is not watered
                }
            }
        }
    }

   /* private void Update()
    {
        if(Keyboard.current.yKey.wasPressedThisFrame)
        {
            GrowCrop(); // Call GrowCrop when the 'Y' key is pressed
        }
    }  */

}

[System.Serializable] // This attribute allows the class to be serialized by Unity's serialization system, making it visible in the Inspector.
public class BlockInfo
{
    public bool isWatered;
    public GrowBlock.GrowthStage currentStage;
    public CropController.CropType cropType;
    public float growFailChance; // Chance of growth failure, can be used for crop growth logic

}

[System.Serializable] 

public class InfoRow
{
    public List<BlockInfo> blocks = new List<BlockInfo>(); // List to hold information about blocks in this row
}