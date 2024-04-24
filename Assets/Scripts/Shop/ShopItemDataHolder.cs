using UnityEngine;
using UnityEngine.UI;

public class ShopItemDataHolder : MonoBehaviour
{
    public ShopItemData itemData;

    private Button purchaseButton;

    private void Awake()
    {
        itemData.name= gameObject.name; // Assicurati che il nome dell'oggetto corrisponda al nome dello scriptable object
        itemData.itemCostText = gameObject.transform.GetChild(1).GetComponent<TMPro.TMP_Text>();
        //itemData.itemCostText.text=itemData.bought?"":"Cost: " + itemData.cost.ToString();

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
        purchaseButton = GetComponent<Button>(); // Assicurati che questo script sia attaccato al GameObject che contiene il Button
        purchaseButton.onClick.AddListener(PurchaseItem); // Aggiungi il listener qui direttamente
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
                        }
                        else
                        {
                            if (!item.itemData.bought)
                            {
                                item.itemData.itemCostText.text = "Cost:"+item.itemData.cost.ToString();
                            }
                            else
                            {
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
                        }
                        else
                        {
                            if (!item.itemData.bought)
                            {
                                item.itemData.itemCostText.text = "Cost: "+item.itemData.cost.ToString();
                            }
                            else
                            {
                                item.itemData.itemCostText.text = ""; 
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
