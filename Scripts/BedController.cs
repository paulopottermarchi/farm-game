using UnityEngine;
using UnityEngine.InputSystem; // Adicione esta linha

public class BedController : MonoBehaviour
{
    private bool canSleep;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canSleep == true)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.eKey.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                //GridInfo.instance.GrowCrop(); // Call the GrowCrop method when the player interacts with the bed
                
                if(TimeController.instance != null)
                {
                    TimeController.instance.EndDay(); // Call EndDay to handle the end of the day logic
                }
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canSleep = true; // Set canSleep to true when the player enters the bed area
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canSleep = false; // Set canSleep to false when the player exits the bed area
        }
    }
}
