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
}
