using UnityEngine;
using UnityEngine.SceneManagement; // Adicione esta linha

public class AreaSwitcher : MonoBehaviour
{
    public string sceneToLoad; // The name of the scene to switch to
    public Transform startPoint; // The spawn point in the new area
    public string transitionName; // The name of the transition effect (if any)
    void Start()
    {
        if (PlayerPrefs.HasKey("Transition"))
        {
            if (PlayerPrefs.GetString("Transition") == transitionName)
            {

                PlayerController.instance.transform.position = startPoint.position;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Switch to the new area, This could involve changing the scene, loading new assets, etc.
            //Debug.Log("Player entered the area switcher zone.");
            SceneManager.LoadScene(sceneToLoad);

            PlayerPrefs.SetString("Transition", transitionName); // Save the transition name to PlayerPrefs
        }
    }
}
