using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class CustomizationController : MonoSingleton<CustomizationController>
{
    public int coins; // Monete disponibili per l'utente
    void Start()
    {
        //PlayerPrefs.SetInt("Coins", coins); // Imposta le monete iniziali //da rimuovere perchè è solo per test
        LoadCoins();
        
    }

    [ContextMenu("Clear Player Prefs")]
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs Cleared");

        //Clear shop item bought status
        var shopItems = FindObjectsOfType<ShopItemDataHolder>();
        foreach (var shopItem in shopItems)
        {
            shopItem.itemData.bought = false;
        }
    }

    public void LoadCoins()
    {
        coins = PlayerPrefs.GetInt("Coins");
        UICustomizationController.Instance.UpdateUI();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerPrefs.SetInt("Coins", 9999);
            UICustomizationController.Instance.UpdateUI();
        }
    }
    public void Pro_Antenna()
    {
        ScoreController.Instance.scoreAmount = 20;
        BatteryController.Instance.consumeEnergyAmount = 20;
    }
    public void Tank_Antenna()
    {
        ScoreController.Instance.scoreAmount = 5;
        BatteryController.Instance.consumeEnergyAmount = 5;
    }
    public void Base_Antenna()
    {
        ScoreController.Instance.scoreAmount = 10;
        BatteryController.Instance.consumeEnergyAmount = 10;
    }
    public void Organic_Perk()
    {
        PerksController.Instance.organic = true;
        PerksController.Instance.metal = false;
        PerksController.Instance.paper = false;
        PerksController.Instance.plastic = false;
        PerksController.Instance.glass = false;

    }
    public void Glass_Perk()
    {
        PerksController.Instance.organic = false;
        PerksController.Instance.metal = false;
        PerksController.Instance.paper = false;
        PerksController.Instance.plastic = false;
        PerksController.Instance.glass = true;
    }
    public void Metal_Perk()
    {
        PerksController.Instance.organic = false;
        PerksController.Instance.metal = true;
        PerksController.Instance.paper = false;
        PerksController.Instance.plastic = false;
        PerksController.Instance.glass = false;
    }
    public void Plastic_Perk()
    {
        PerksController.Instance.organic = false;
        PerksController.Instance.metal = false;
        PerksController.Instance.paper = false;
        PerksController.Instance.plastic = true;
        PerksController.Instance.glass = false;
    }
    public void Paper_Perk()
    {
        PerksController.Instance.organic = false;
        PerksController.Instance.metal = false;
        PerksController.Instance.paper = true;
        PerksController.Instance.plastic = false;
        PerksController.Instance.glass = false;
    }

}
