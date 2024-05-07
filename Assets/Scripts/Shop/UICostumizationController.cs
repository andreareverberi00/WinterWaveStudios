using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class UICustomizationController : MonoSingleton<UICustomizationController>
{
    [SerializeField] private TMP_Text coinsText;
    bool menu=false;
    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        coinsText.text = PlayerPrefs.GetInt("Coins", 0).ToString("0000");
    }
    public void BackToMenu()
    {

        menu = true;
    }
    private void Update()
    {
        if(menu)
        {
            StartCoroutine(WaitandLoad());
        }
    }
    IEnumerator WaitandLoad()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Menu");
    }
}
