using UnityEngine;

public class CustomizationController : MonoSingleton<CustomizationController>
{
    public int coins; // Monete disponibili per l'utente
    public AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        audioSource.PlayOneShot(audioClip);
        LoadCustomizationData();
    }

    public void LoadCustomizationData()
    {
        // Carica dati salvati
        coins = PlayerPrefs.GetInt("Coins", 0);
        // Carica lo stato di sblocco per antenne e perk
    }

    public void PurchaseItem(int cost)
    {
        if (coins >= cost)
        {
            coins -= cost;
            PlayerPrefs.SetInt(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.gameObject.name, 1);
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Not enough coins to purchase this item.");
        }

    }

    public void SelectAntenna(string antennaName)
    {
        PlayerPrefs.SetString("SelectedAntenna", antennaName);
        PlayerPrefs.Save();
    }

    public void SelectPerk(string perkName)
    {
        PlayerPrefs.SetString("SelectedPerk", perkName);
        PlayerPrefs.Save();
    }

    // Altri metodi per gestire la customizzazione...
}
