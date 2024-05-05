using UnityEngine;
using UnityEngine.UI;

public class ShopItemDataHolder : MonoBehaviour
{
    public ShopItemData itemData;

    private Button purchaseButton;

    private void Awake()
    {
        itemData.name= gameObject.name; // Assicurati che il nome dell'oggetto corrisponda al nome dello scriptable object
        itemData.itemCostText = gameObject.transform.GetChild(0).GetComponent<TMPro.TMP_Text>();
        itemData.itemCostText.text=itemData.bought?"":itemData.cost.ToString();

        // check the playerprefs to see selected antenna and perk
        if(itemData.itemType == ShopItemType.Antenna)
        {
            if(PlayerPrefs.GetString("SelectedAntenna", "baseAntenna") == itemData.name)
            {
                itemData.itemCostText.text = "Selected";
            }
        }
        else if(itemData.itemType == ShopItemType.Perk)
        {
            if(PlayerPrefs.GetString("SelectedPerk", "") == itemData.name)
            {
                itemData.itemCostText.text = "Selected";
            }
        }
        SetupCoinIcon();

        purchaseButton = GetComponent<Button>(); // Assicurati che questo script sia attaccato al GameObject che contiene il Button
        purchaseButton.onClick.AddListener(PurchaseItem); // Aggiungi il listener qui direttamente
    }
    void SetupCoinIcon()
    {
        if (itemData.coinSprite != null && itemData.coinIconInstance == null) // Crea l'icona se non esiste
        {
            GameObject coinIcon = new GameObject("CoinIcon");
            Image coinImage = coinIcon.AddComponent<Image>();
            coinImage.sprite = itemData.coinSprite;
            coinImage.rectTransform.sizeDelta = new Vector2(40, 40);
            coinImage.rectTransform.anchoredPosition = new Vector2(itemData.itemCostText.preferredWidth + 4, 0); // Assumi che il testo sia allineato a sinistra

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

        if(itemData.bought)
        {
            Debug.Log("Hai già acquistato questo oggetto, ma lo seleziono");

            if(itemData.itemType == ShopItemType.Antenna)
            {
                RobotVisualController.Instance.SelectAntenna(itemData.name);
                // set cost text to "Selected" and all others to nothing if bought otherwise to cost
                foreach (var item in FindObjectsOfType<ShopItemDataHolder>())
                {
                    if(item.itemData.itemType == ShopItemType.Antenna)
                    {
                        if(item.itemData.name == itemData.name)
                        {
                            item.itemData.itemCostText.text = "Selected";
                            UpdateCoinIconVisibility();
                        }
                        else
                        {
                            if (!item.itemData.bought)
                            {
                                item.itemData.itemCostText.text = item.itemData.cost.ToString();
                            }
                            else
                            {
                                UpdateCoinIconVisibility();
                                item.itemData.itemCostText.text = ""; 
                            }
                        }
                    }
                }
            }
            else if(itemData.itemType == ShopItemType.Perk)
            {
                RobotVisualController.Instance.SelectPerk(itemData.name);
                // set cost text to "Selected" and all others to nothing if bought otherwise to cost
                foreach (var item in FindObjectsOfType<ShopItemDataHolder>())
                {
                    if(item.itemData.itemType == ShopItemType.Perk)
                    {
                        if(item.itemData.name == itemData.name)
                        {
                            item.itemData.itemCostText.text = "Selected";
                            UpdateCoinIconVisibility();
                        }
                        else
                        {
                            if (!item.itemData.bought)
                            {
                                item.itemData.itemCostText.text = item.itemData.cost.ToString();
                            }
                            else
                            {
                                item.itemData.itemCostText.text = ""; 
                                UpdateCoinIconVisibility();
                            }
                        }
                    }
                }
            }
            return;
        }
        else if(coins >= itemData.cost)
        {
            coins -= itemData.cost;
            PlayerPrefs.SetInt("Coins", coins);
            itemData.bought = true;
            UICustomizationController.Instance.UpdateUI();
            Debug.Log("Acquisto completato");
        }
        else
        {
            Debug.Log("Non hai abbastanza monete");
        }

    }
    //private void OnDisable()
    //{
    //    purchaseButton.onClick.RemoveListener(PurchaseItem);
    //}
}
