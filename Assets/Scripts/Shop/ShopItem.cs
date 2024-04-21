using TMPro;
using UnityEngine;

public enum ShopItemType
{
    Antenna,
    Perk
}

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop/Shop Item")]
public class ShopItemData : ScriptableObject
{
    [HideInInspector] public string itemName;
    public int cost;
    public ShopItemType itemType;
    public bool bought;
    [HideInInspector] public TMP_Text itemCostText;
}
