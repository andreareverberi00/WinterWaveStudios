using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ShopItemDataHolder : MonoBehaviour
{
    public ShopItemData itemData;

    private Button purchaseButton;

    [SerializeField] private GameObject purchaseConfirmationPanel;
    [SerializeField] private TMP_Text confirmationDescriptionText;
    private void Awake()
    {
        itemData.name= gameObject.name; // Assicurati che il nome dell'oggetto corrisponda al nome dello scriptable object
        itemData.itemCostText = gameObject.transform.GetChild(0).GetComponent<TMPro.TMP_Text>();
        itemData.itemCostText.text=itemData.bought?"":itemData.cost.ToString("0000");

        // check the playerprefs to see selected antenna and perk
        if(itemData.itemType == ShopItemType.Antenna)
        {
            if(PlayerPrefs.GetString("SelectedAntenna", "baseAntenna") == itemData.name)
            {
                itemData.itemCostText.text = "Selected";
                GetComponent<Image>().color = Color.yellow;
            }
        }
        else if(itemData.itemType == ShopItemType.Perk)
        {
            if(PlayerPrefs.GetString("SelectedPerk", "") == itemData.name)
            {
                itemData.itemCostText.text = "Selected";
                GetComponent<Image>().color = Color.yellow;
            }
        }
        SetupCoinIcon();

        Button purchaseButton = GetComponent<Button>();
        purchaseButton.onClick.AddListener(() =>
            FindObjectOfType<ShopController>().RequestPurchaseConfirmation(this));
    }

    void SetupCoinIcon()
    {
        if (itemData.coinSprite != null && itemData.coinIconInstance == null) // Crea l'icona se non esiste
        {
            GameObject coinIcon = new GameObject("CoinIcon");
            Image coinImage = coinIcon.AddComponent<Image>();
            coinImage.sprite = itemData.coinSprite;
            coinImage.rectTransform.sizeDelta = new Vector2(40, 40);
            coinImage.rectTransform.anchoredPosition = new Vector2(itemData.itemCostText.preferredWidth, 0); // Assumi che il testo sia allineato a sinistra

            coinIcon.transform.SetParent(transform.GetChild(0), false);
            itemData.coinIconInstance = coinIcon;
        }
        UpdateCoinIconVisibility();
    }
    void UpdateCoinIconVisibility()
    {
        if (itemData.coinIconInstance != null)
        {
            itemData.coinIconInstance.SetActive(!itemData.bought);
        }
    }

    public void PurchaseItem()
    {
        int coins = PlayerPrefs.GetInt("Coins");

        if (itemData.bought)
        {
            Debug.Log("Hai già acquistato questo oggetto, ma lo seleziono");
            SelectItem(itemData.itemType);
            return;
        }
        else if (coins >= itemData.cost)
        {
            coins -= itemData.cost;
            PlayerPrefs.SetInt("Coins", coins);
            itemData.bought = true;
            UICustomizationController.Instance.UpdateUI();
            itemData.itemCostText.text = "";
            UpdateCoinIconVisibility();
            Debug.Log("Acquisto completato");
        }
        else
        {
            Debug.Log("Non hai abbastanza monete");
        }
    }

    private void SelectItem(ShopItemType itemType)
    {
        var currentSelected = PlayerPrefs.GetString(itemType.ToString());

        if (currentSelected == itemData.name)
        {
            // Deselect item
            PlayerPrefs.SetString(itemType.ToString(), "");
            RobotVisualController.Instance.SelectItemOfType(itemType, "");
            Debug.Log(itemData.name + " deselezionato");
        }
        else
        {
            RobotVisualController.Instance.SelectItemOfType(itemType, itemData.name);
            PlayerPrefs.SetString(itemType.ToString(), itemData.name);
            Debug.Log(itemData.name + " selezionato");
        }

        foreach (var item in FindObjectsOfType<ShopItemDataHolder>())
        {
            if (item.itemData.itemType == itemType)
            {
                var itemName = item.itemData.name;
                var isCurrentItem = itemName == itemData.name;
                var isSelected = PlayerPrefs.GetString(itemType.ToString()) == itemName;

                item.itemData.itemCostText.text = isSelected ? "Selected" : item.itemData.bought ? "" : item.itemData.cost.ToString("0000");
                item.GetComponent<Image>().color = isSelected ? Color.yellow : item.itemData.bought ? Color.white : item.GetComponent<Image>().color;

                if (item.itemData.bought)
                {
                    UpdateCoinIconVisibility();
                }
            }
        }
    }

}
