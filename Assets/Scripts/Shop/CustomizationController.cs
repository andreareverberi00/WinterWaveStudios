using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class CustomizationController : MonoSingleton<CustomizationController>
{
    public int coins; // Monete disponibili per l'utente
    void Start()
    {
        //PlayerPrefs.SetInt("Coins", coins); // Imposta le monete iniziali //da rimuovere perchè è solo per test
        //Scene_Link2.Instance.proantenna = false;
        //Scene_Link2.Instance.tankantenna = false;
        //Scene_Link2.Instance.baseantenna = true;
        //Scene_Link2.Instance.perkorganic = false;
        //Scene_Link2.Instance.perkmetal = false;
        //Scene_Link2.Instance.perkpaper = false;
        //Scene_Link2.Instance.perkplastic = true;
        //Scene_Link2.Instance.perkglass = false;
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
        PlayerPrefs.SetInt("proantenna", Scene_Link2.Instance.proantenna ? 1 : 0);
        Scene_Link2.Instance.proantenna = PlayerPrefs.GetInt("proantenna", 0) == 1;
        Scene_Link2.Instance.proantenna = true;
        Scene_Link2.Instance.tankantenna = false;
        Scene_Link2.Instance.baseantenna = false;

    }
    public void Tank_Antenna()
    {
        PlayerPrefs.SetInt("tankantenna", Scene_Link2.Instance.tankantenna ? 1 : 0);
        Scene_Link2.Instance.tankantenna = PlayerPrefs.GetInt("tankantenna", 0) == 1;
        Scene_Link2.Instance.proantenna = false;
        Scene_Link2.Instance.tankantenna = true;
        Scene_Link2.Instance.baseantenna = false;
    }
    public void Base_Antenna()
    {
        PlayerPrefs.SetInt("baseantenna", Scene_Link2.Instance.baseantenna ? 1 : 0);
        Scene_Link2.Instance.baseantenna = PlayerPrefs.GetInt("baseantenna", 0) == 1;
        Scene_Link2.Instance.proantenna = false;
        Scene_Link2.Instance.tankantenna = false;
        Scene_Link2.Instance.baseantenna = true;
    }
    public void Organic_Perk()
    {
        PlayerPrefs.SetInt("perkorganic", Scene_Link2.Instance.perkorganic ? 1 : 0);
        Scene_Link2.Instance.perkorganic = PlayerPrefs.GetInt("perkorganic", 0) == 1;
        Scene_Link2.Instance.perkorganic = true;
        Scene_Link2.Instance.perkmetal = false;
        Scene_Link2.Instance.perkpaper = false;
        Scene_Link2.Instance.perkplastic = false;
        Scene_Link2.Instance.perkglass = false;

    }
    public void Glass_Perk()
    {
        PlayerPrefs.SetInt("perkglass", Scene_Link2.Instance.perkglass ? 1 : 0);
        Scene_Link2.Instance.perkglass = PlayerPrefs.GetInt("perkglass", 0) == 1;
        Scene_Link2.Instance.perkorganic = false;
        Scene_Link2.Instance.perkmetal = false;
        Scene_Link2.Instance.perkpaper = false;
        Scene_Link2.Instance.perkplastic = false;
        Scene_Link2.Instance.perkglass = true;
    }
    public void Metal_Perk()
    {
        PlayerPrefs.SetInt("perkmetal", Scene_Link2.Instance.perkmetal ? 1 : 0);
        Scene_Link2.Instance.perkmetal = PlayerPrefs.GetInt("perkmetal", 0) == 1;
        Scene_Link2.Instance.perkorganic = false;
        Scene_Link2.Instance.perkmetal = true;
        Scene_Link2.Instance.perkpaper = false;
        Scene_Link2.Instance.perkplastic = false;
        Scene_Link2.Instance.perkglass = false;
    }
    public void Plastic_Perk()
    { 
        PlayerPrefs.SetInt("perkplastic", Scene_Link2.Instance.perkplastic ? 1 : 0);
        Scene_Link2.Instance.perkplastic = PlayerPrefs.GetInt("perkplastic", 0) == 1;
        Scene_Link2.Instance.perkorganic = false;
        Scene_Link2.Instance.perkmetal = false;
        Scene_Link2.Instance.perkpaper = false;
        Scene_Link2.Instance.perkplastic = true;
        Scene_Link2.Instance.perkglass = false;
    }
    public void Paper_Perk()
    {
        PlayerPrefs.SetInt("perkpaper", Scene_Link2.Instance.perkpaper ? 1 : 0);
        Scene_Link2.Instance. perkpaper = PlayerPrefs.GetInt("perkpaper", 0) == 1;
        Scene_Link2.Instance.perkorganic = false;
        Scene_Link2.Instance.perkmetal = false;
        Scene_Link2.Instance.perkpaper = true;
        Scene_Link2.Instance.perkplastic = false;
        Scene_Link2.Instance.perkglass = false;
    }

}
