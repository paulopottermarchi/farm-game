using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem; // Add this line 


public class GridController : MonoBehaviour
{
    public static GridController instance; // Singleton instance of GridController
    private void Awake()
    {
        instance = this; // Set the singleton instance to this GridController
    }
    public Transform minPoint, maxPoint;

    public GrowBlock baseGridBlock;

    private Vector2Int gridSize;

    public List<BlockRow> blockRows = new List<BlockRow>(); // List to hold rows of blocks in the grid
    // This layer mask is used to check for blockers in the grid

    public LayerMask gridBlockers; // Layer mask to check for blockers in the grid


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateGrid()
    {
        // This method will generate a grid based on the minPoint and maxPoint
        // You can implement the logic to create grid cells or tiles here

        minPoint.position = new Vector3(Mathf.Round(minPoint.position.x), Mathf.Round(minPoint.position.y), 0f);
        maxPoint.position = new Vector3(Mathf.Round(maxPoint.position.x), Mathf.Round(maxPoint.position.y), 0f);

        Vector3 startpoint = minPoint.position + new Vector3(0.5f, 0.5f, 0f);

        //Instantiate(baseGridBlock, startpoint, Quaternion.identity);

        gridSize = new Vector2Int(Mathf.RoundToInt(maxPoint.position.x - minPoint.position.x),
            Mathf.RoundToInt(maxPoint.position.y - minPoint.position.y));

        for (int y = 0; y < gridSize.y; y++)
        {
            blockRows.Add(new BlockRow()); // Add a new row to the blockRows list

            for (int x = 0; x < gridSize.x; x++)
            {
                GrowBlock newBlock = Instantiate(baseGridBlock, startpoint + new Vector3(x, y, 0f), Quaternion.identity); // Instantiate a new block at the calculated position

                newBlock.transform.SetParent(transform); // Set the parent of the new block to this grid controller
                newBlock.theSR.sprite = null; // Set the sprite of the new block to null initially
                newBlock.SetGridPosition(x, y); // Set the grid position of the new block

                blockRows[y].blocks.Add(newBlock); // Add the new block to the current row

                if (Physics2D.OverlapBox(newBlock.transform.position, new Vector2(0.9f, 0.9f), 0f, gridBlockers))
                {
                    newBlock.theSR.sprite = null; // Set the sprite to null if there's a blocker
                    newBlock.preventUse = true; // Prevent the use of this block
                }

                if (GridInfo.instance.hasGrid == true)
                {
                    BlockInfo storedBlock = GridInfo.instance.theGrid[y].blocks[x]; // Retrieve the stored block information from GridInfo

                    newBlock.currentStage = storedBlock.currentStage; // Set the current stage of the new block
                    newBlock.isWatered = storedBlock.isWatered; // Set whether the block is watered
                    newBlock.cropType = storedBlock.cropType; // Set the crop type of the new block
                    newBlock.growFailChance = storedBlock.growFailChance; // Set the growth failure chance of the new block

                    newBlock.SetSoilSprite(); // Set the soil sprite based on the current stage and whether it's watered
                    newBlock.UpdateCropSprite(); // Update the crop sprite based on the current stage
                }
                
            }
        }

        if (GridInfo.instance.hasGrid == false)
        {
            GridInfo.instance.CreateGrid(); // Create the grid in GridInfo if it doesn't exist
        }

        baseGridBlock.gameObject.SetActive(false); // Hide the base grid block after instantiation
    }

    public GrowBlock GetBlock(float x, float y)
    {
        // This method can be used to retrieve a block from the grid
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        x -= minPoint.position.x;
        y -= minPoint.position.y;

        int intX = Mathf.RoundToInt(x);
        int intY = Mathf.RoundToInt(y);

        if (x < gridSize.x && y < gridSize.y)
        {
            return blockRows[intY].blocks[intX]; // Return the block at the specified coordinates
        }

        return null; // Return null for now, as this is just a placeholder

    }
}

[System.Serializable]

public class BlockRow
{
    public List<GrowBlock> blocks = new List<GrowBlock>(); // List to hold blocks in the row
}
