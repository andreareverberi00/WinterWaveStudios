using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public void BackToMenu()
    {
        Scenelink.Instance.play = false;
        SceneManager.LoadScene("Menu");
    }
}
