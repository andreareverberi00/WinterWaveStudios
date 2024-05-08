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
    public Sprite coinSprite;
    public ShopItemType itemType;
    public bool bought;

    [TextAreaAttribute]
    public string description;

    [TextAreaAttribute]
    public string bonusDescription;

    [HideInInspector] public TMP_Text itemCostText;
    [HideInInspector] public GameObject coinIconInstance; // Riferimento all'istanza dell'icona della moneta
}