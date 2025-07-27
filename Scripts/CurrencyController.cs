using UnityEngine;

public class CurrencyController : MonoBehaviour
{

    public static CurrencyController instance; // Singleton instance of CurrencyController

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // Set the singleton instance to this CurrencyController
            DontDestroyOnLoad(gameObject); // Ensure the currency controller persists across scene loads
        }
        else
        {
            Destroy(gameObject); // If another instance exists, destroy this one to maintain singleton pattern
        }
    }


    public float currentMoney; // Current amount of money the player has

    private void Start()
    {
        UIController.instance.UpdateMoneyText(currentMoney); // Update the UI with the initial amount of money
    }

    public void SpendMoney(float amountToSpend)
    {
        currentMoney -= amountToSpend; // Deduct the specified amount from the current money

        UIController.instance.UpdateMoneyText(currentMoney); // Update the UI to reflect the new amount of money
    }

    public void AddMoney(float amountToAdd)
    {
        currentMoney += amountToAdd; // Add the specified amount to the current money
        UIController.instance.UpdateMoneyText(currentMoney); 
    }

    public bool CheckMoney(float amount)
    {
        if(currentMoney >= amount)
        {
            return true; // Player has enough money
        }
        else
        {
            return false; // Player does not have enough money
        }
    }
}
