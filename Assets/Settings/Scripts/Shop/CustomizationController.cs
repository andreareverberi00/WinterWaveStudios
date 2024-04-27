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
        Scene_Link2.Instance.proantenna = true;
        Scene_Link2.Instance.tankantenna = false;
        Scene_Link2.Instance.baseantenna = false;

    }
    public void Tank_Antenna()
    {
        Scene_Link2.Instance.proantenna = false;
        Scene_Link2.Instance.tankantenna = true;
        Scene_Link2.Instance.baseantenna = false;
    }
    public void Base_Antenna()
    {
        Scene_Link2.Instance.proantenna = false;
        Scene_Link2.Instance.tankantenna = false;
        Scene_Link2.Instance.baseantenna = true;
    }
    public void Organic_Perk()
    {
        Scene_Link2.Instance.perkorganic = true;
        Scene_Link2.Instance.perkmetal = false;
        Scene_Link2.Instance.perkpaper = false;
        Scene_Link2.Instance.perkplastic = false;
        Scene_Link2.Instance.perkglass = false;

    }
    public void Glass_Perk()
    {
        Scene_Link2.Instance.perkorganic = false;
        Scene_Link2.Instance.perkmetal = false;
        Scene_Link2.Instance.perkpaper = false;
        Scene_Link2.Instance.perkplastic = false;
        Scene_Link2.Instance.perkglass = true;
    }
    public void Metal_Perk()
    {
        Scene_Link2.Instance.perkorganic = false;
        Scene_Link2.Instance.perkmetal = true;
        Scene_Link2.Instance.perkpaper = false;
        Scene_Link2.Instance.perkplastic = false;
        Scene_Link2.Instance.perkglass = false;
    }
    public void Plastic_Perk()
    {
        Scene_Link2.Instance.perkorganic = false;
        Scene_Link2.Instance.perkmetal = false;
        Scene_Link2.Instance.perkpaper = false;
        Scene_Link2.Instance.perkplastic = true;
        Scene_Link2.Instance.perkglass = false;
    }
    public void Paper_Perk()
    {
        Scene_Link2.Instance.perkorganic = false;
        Scene_Link2.Instance.perkmetal = false;
        Scene_Link2.Instance.perkpaper = true;
        Scene_Link2.Instance.perkplastic = false;
        Scene_Link2.Instance.perkglass = false;
    }

}
