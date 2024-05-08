using UnityEngine;
using TMPro;
using System.Collections;

public class DisplayBonuses : MonoSingleton<DisplayBonuses>
{
    public TMP_Text text;

    private void Start()
    {
        UpdateBonusesText();
    }

    public void UpdateBonusesText()
    {
        string selectedAntenna = PlayerPrefs.GetString("SelectedAntenna", "");
        string selectedPerk = PlayerPrefs.GetString("SelectedPerk", "");

        string antennaBonus = GetBonusDescription(selectedAntenna);
        string perkBonus = GetBonusDescription(selectedPerk);

        if (string.IsNullOrEmpty(antennaBonus) && string.IsNullOrEmpty(perkBonus))
        {
            text.text = "No bonuses selected";
        }
        else
        {
            text.text = (antennaBonus + "\n" + perkBonus).Trim();
        }
    }

    private string GetBonusDescription(string itemName)
    {
        if (!string.IsNullOrEmpty(itemName))
        {
            GameObject[] allObjectsWithName = GameObject.FindObjectsOfType<GameObject>(); // Trova tutti gli oggetti in scena
            foreach (var obj in allObjectsWithName)
            {
                if (obj.name == itemName)
                {
                    ShopItemDataHolder itemData = obj.GetComponent<ShopItemDataHolder>();
                    if (itemData && itemData.itemData.bought)
                    {
                        return itemData.itemData.bonusDescription;
                    }
                }
            }
        }
        return "";
    }

}
