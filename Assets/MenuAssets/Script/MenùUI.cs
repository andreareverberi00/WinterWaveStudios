using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenùUI : MonoBehaviour
{
    public GameObject CreditsText;
    public GameObject Credit;
    public GameObject Menu;
    public GameObject Play;
    public GameObject Custom;
    public GameObject Options;
    public GameObject OptionS;

    public TMP_Text highscoreText;

    void Start()
    {
        CreditsText.SetActive(false);
        OptionS.SetActive(false);
        Menu.SetActive(false);
        Play.SetActive(true);
        Custom.SetActive(true);
        Options.SetActive(true);
        Credit.SetActive(true);
        highscoreText.text = "Highscore: "+PlayerPrefs.GetInt("Highscore", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CREDITS()
    {
        CreditsText.SetActive(true);
        OptionS.SetActive(false);
        Menu.SetActive(true);
        Play.SetActive(false);
        Custom.SetActive(false);
        Options.SetActive(false);
        Credit.SetActive(false);
    }

   public void BackToMenu()
    {
        CreditsText.SetActive(false);
        OptionS.SetActive(false);
        Menu.SetActive(false);
        Play.SetActive(true);
        Custom.SetActive(true);
        Options.SetActive(true);
        Credit.SetActive(true);
    }
    public void OPTIONS()
    {
        CreditsText.SetActive(false);
        OptionS.SetActive(true);
        Menu.SetActive(true);
        Play.SetActive(false);
        Custom.SetActive(false);
        Options.SetActive(false);
        Credit.SetActive(false);
    }
}
