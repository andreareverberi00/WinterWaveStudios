using UnityEngine;
using TMPro; // Per interagire con il testo della UI
using UnityEngine.UI; // Per interagire con gli elementi UI

public class UICustomizationController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text coinsText; // Riferimento al testo che mostra le monete disponibili

    [SerializeField]
    private Button[] antennaButtons; // Pulsanti per le antenne
    [SerializeField]
    private Button[] perkButtons; // Pulsanti per i perk

    [SerializeField]
    private RobotVisualController robotVisualController; // Riferimento al controller visuale del robot

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        // Aggiorna il testo delle monete
        coinsText.text = PlayerPrefs.GetInt("Coins", 1000).ToString();

        // Aggiorna i pulsanti in base a ciò che è sbloccato
        foreach (var button in antennaButtons)
        {
            string antennaName = button.GetComponentInChildren<TMP_Text>().text;
            button.interactable = IsItemUnlocked(antennaName);
        }

        foreach (var button in perkButtons)
        {
            string perkName = button.GetComponentInChildren<TMP_Text>().text;
            button.interactable = IsItemUnlocked(perkName);
        }
    }

    private bool IsItemUnlocked(string itemName)
    {
        return PlayerPrefs.GetInt(itemName, 0) == 1;
    }

    public void SelectAntenna(string antennaName)
    {
        PlayerPrefs.SetString("SelectedAntenna", antennaName);
        robotVisualController.UpdateVisuals();
    }

    public void SelectPerk(string perkName)
    {
        PlayerPrefs.SetString("SelectedPerk", perkName);
        robotVisualController.UpdateVisuals();
    }

    public void BuyItem(string itemName, int cost)
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        if (coins >= cost)
        {
            coins -= cost;
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.SetInt(itemName, 1); // Sblocca l'oggetto
            UpdateUI();
        }
        else
        {
            // Notifica l'utente che non ha abbastanza monete
            Debug.Log("Non hai abbastanza monete!");
        }
    }

    // Aggiungi qui altri metodi per la gestione delle interazioni UI...
}
