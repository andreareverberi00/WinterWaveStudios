using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject purchaseConfirmationPanel;
    [SerializeField] private TMP_Text confirmationDescriptionText;

    private ShopItemDataHolder currentItemDataHolder;

    public void RequestPurchaseConfirmation(ShopItemDataHolder itemDataHolder)
    {
        if(itemDataHolder.itemData.bought)
        {
            itemDataHolder.PurchaseItem();
            return;
        }
        currentItemDataHolder = itemDataHolder;
        confirmationDescriptionText.text = $"Confirm the purchase of {itemDataHolder.name} for {itemDataHolder.itemData.cost} coins?\n{itemDataHolder.itemData.description}";
        purchaseConfirmationPanel.SetActive(true);
    }

    public void ConfirmPurchase()
    {
        if (currentItemDataHolder != null)
        {
            // Assumi che il metodo PurchaseItem esista e faccia tutto il necessario per l'acquisto
            currentItemDataHolder.PurchaseItem();
            purchaseConfirmationPanel.SetActive(false);
            currentItemDataHolder = null;
        }
    }

    public void CancelPurchase()
    {
        purchaseConfirmationPanel.SetActive(false);
    }
}
