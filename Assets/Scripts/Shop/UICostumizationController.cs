using UnityEngine;
using TMPro;

public class UICustomizationController : MonoSingleton<UICustomizationController>
{
    [SerializeField] private TMP_Text coinsText;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        coinsText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }

}
