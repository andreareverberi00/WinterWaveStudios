using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenùUI : MonoBehaviour
{
    public GameObject CreditsText;
    public GameObject Credit;
    public GameObject Menu;
    public GameObject Play;
    public GameObject Custom;
    public GameObject Options;
    public GameObject OptionS;
    public GameObject nametext;
    public TMP_Text highscoreText;
    public Toggle musicToggle;
    public Toggle easythrow;

    void Start()
    {
        CreditsText.SetActive(false);
        OptionS.SetActive(false);
        Menu.SetActive(false);
        Play.SetActive(true);
        Custom.SetActive(true);
        Options.SetActive(true);
        Credit.SetActive(true);
        nametext.SetActive(true);
        highscoreText.text = "Highscore: "+PlayerPrefs.GetInt("Highscore", 0).ToString();
        musicToggle.isOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        easythrow.isOn = false;
    }

    public void SetMusicEnabled()
    {
        bool isEnabled = musicToggle.isOn;
        AudioListener.volume = isEnabled ? 1 : 0;
        PlayerPrefs.SetInt("MusicEnabled", isEnabled ? 1 : 0);
    }

    public void CREDITS()
    {
        CreditsText.SetActive(true);
        nametext.SetActive(false);
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
        nametext.SetActive(true);
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
        nametext.SetActive(false);
        OptionS.SetActive(true);
        Menu.SetActive(true);
        Play.SetActive(false);
        Custom.SetActive(false);
        Options.SetActive(false);
        Credit.SetActive(false);
    }
    private void Update()
    {
        if(Scene_Link2.Instance.tankantenna==true)
        {
            easythrow.isOn = true;
        }
        else
        {
            easythrow.isOn = false;
        }
    }
}
