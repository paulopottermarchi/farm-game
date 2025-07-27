using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance; // Singleton instance of PlayerController

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Set the singleton instance to this PlayerController
            DontDestroyOnLoad(gameObject); // Ensure the player persists across scene loads
        }
        else
        {
            Destroy(gameObject); // If another instance exists, destroy this one to maintain singleton pattern
        }
    }
    public Rigidbody2D theRB;
    public float moveSpeed;
    public InputActionReference moveInput, actionInput;

    public Animator anim;

    public enum ToolType
    {
        plough,
        wateringCan,
        seeds,
        basket
    }

    public ToolType currentTool;

    public float toolWaitTime = .5f; // Time to wait before using the tool again
    private float toolWaitCounter;

    public Transform toolIndicator; // Reference to the tool indicator in the UI
    public float toolRange = 3f; // Range within which the tool can interact with blocks~

    public CropController.CropType seedCropType; // Current crop type being used by the player
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIController.instance.SwitchTool((int)currentTool);

        UIController.instance.SwitchSeed(seedCropType); // Initialize the seed type in the UI
    }

    /* Update is called once per frame
    * This method updates the player's movement based on input actions
    * It sets the player's rigidbody velocity based on the input direction and speed   
    * It also flips the player's sprite based on the direction of movement
    * If the action input is pressed, it calls the useTool method to interact with the environment
     It sets the animator's speed parameter based on the player's movement speed */

    void Update()
    {
        if (UIController.instance != null)
        {
            if (UIController.instance.theIC != null)
            {
                if (UIController.instance.theIC.gameObject.activeSelf == true)
                {
                    theRB.linearVelocity = Vector2.zero; // Stop the player from moving if the inventory is open

                    return; // If the inventory is open, do not process player movement
                }
            }

            if (UIController.instance.theShop != null)
            {
                if (UIController.instance.theShop.gameObject.activeSelf == true)
                {
                    theRB.linearVelocity = Vector2.zero; // Stop the player from moving if the shop is open

                    return; // If the shop is open, do not process player movement
                }
            }

            if (UIController.instance.pauseScreen.activeSelf != null)
            {
                if (UIController.instance.pauseScreen.gameObject.activeSelf == true)
                {
                    theRB.linearVelocity = Vector2.zero; // Stop the player from moving if the pause screen is open

                    return; // If the pause screen is open, do not process player movement
                }
            }
        }

        if (toolWaitCounter > 0)
        {
            toolWaitCounter -= Time.deltaTime; // Decrease the tool wait counter
            theRB.linearVelocity = Vector2.zero; // Stop the player from moving while waiting
        }
        else
        {
            //theRB.velocity = new Vector2(moveSpeed, 0f);
            theRB.linearVelocity = moveInput.action.ReadValue<Vector2>().normalized * moveSpeed;

            if (theRB.linearVelocity.x < 0f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (theRB.linearVelocity.x > 0f)
            {
                transform.localScale = Vector3.one;
            }
        }

        // Cycle through tools using the tab key
        // or number keys 1-4

        bool hasSwitchedTool = false;

        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            currentTool++;
            if ((int)currentTool >= 4)
            {
                currentTool = ToolType.plough;
            }

            hasSwitchedTool = true;
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentTool = ToolType.plough;

            hasSwitchedTool = true;
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentTool = ToolType.wateringCan;

            hasSwitchedTool = true;
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            currentTool = ToolType.seeds;

            hasSwitchedTool = true;
        }
        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            currentTool = ToolType.basket;

            hasSwitchedTool = true;
        }

        // If a tool has been switched, update the UI to reflect the current tool
        // This assumes you have a UIController that handles tool switching
        if (hasSwitchedTool == true)
        {
            //FindFirstObjectByType<UIController>().SwitchTool((int)currentTool);
            UIController.instance.SwitchTool((int)currentTool); // Switch the tool in the UI


        }

        anim.SetFloat("speed", theRB.linearVelocity.magnitude);

        if (GridController.instance != null)
        {

            // Check if the action input was pressed this frame
            // If so, call the useTool method to interact with the environment
            if (actionInput.action.WasPressedThisFrame())
            {
                useTool();
            }



            toolIndicator.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            toolIndicator.position = new Vector3(toolIndicator.position.x, toolIndicator.position.y, 0f); // Ensure z position is 0

            // Ensure the tool indicator is within the tool range
            // If the distance from the player to the tool indicator exceeds the tool range,

            if (Vector3.Distance(toolIndicator.position, transform.position) > toolRange)
            {
                Vector2 direction = toolIndicator.position - transform.position;
                direction = direction.normalized * toolRange; // Limit the distance to toolRange
                toolIndicator.position = transform.position + new Vector3(direction.x, direction.y, 0f); // Update the position of the tool indicator
            }

            toolIndicator.position = new Vector3(Mathf.FloorToInt(toolIndicator.position.x) + 0.5f,
                                                 Mathf.FloorToInt(toolIndicator.position.y) + 0.5f,
                                                 0f); // Snap to grid for better alignment
        }
        else
        {
            toolIndicator.position = new Vector3(0f, 0f, -20f); // Reset tool indicator position if GridController is not available
        }
    }

    /* This method is called when the player uses a tool
    // It finds the first GrowBlock object in the scene and calls its PloughSoil method 
    // to plough the soil, changing its growth stage from barren to ploughed
     It assumes that there is at least one GrowBlock in the scene */

    void useTool()
    {
        GrowBlock block = null;

        //block = FindFirstObjectByType<GrowBlock>();

        //block.PloughSoil();

        block = GridController.instance.GetBlock(toolIndicator.position.x - .5f, toolIndicator.position.y - .5f);
        // Get the block at the position of the tool indicator

        toolWaitCounter = toolWaitTime; // Reset the tool wait counter

        if (block != null)
        {
            switch (currentTool)
            {
                case ToolType.plough:
                    block.PloughSoil();
                    anim.SetTrigger("usePlough");
                    break;

                case ToolType.wateringCan:
                    block.WaterSoil();
                    anim.SetTrigger("useWaterCan");
                    break;

                case ToolType.seeds:
                    if (CropController.instance.GetCropInfo(seedCropType).seedAmount > 0)
                    {
                        block.PlantCrop(seedCropType);
                        //CropController.instance.UseSeed(seedCropType); // Use the seed from the CropController
                    }
                    break;

                case ToolType.basket:
                    block.HarvestCrop();
                    break;
            }
        }
    }

    public void SwitchSeed(CropController.CropType newSeed)
    {
        seedCropType = newSeed; // Update the current seed type to the new one
    }

    
}
