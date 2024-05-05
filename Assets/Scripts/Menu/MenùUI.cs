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
    public GameObject Inputtext;
    public GameObject Employee;
    public TMP_Text highscoreText;
    public Toggle musicToggle;
    public Toggle easythrow;

    void Start()
    {
        Employee.SetActive(true);
        Inputtext.SetActive(true);
        CreditsText.SetActive(false);
        OptionS.SetActive(false);
        Menu.SetActive(false);
        Play.SetActive(true);
        Custom.SetActive(true);
        Options.SetActive(true);
        Credit.SetActive(true);
        nametext.SetActive(true);
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore", 0).ToString();
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

        Inputtext.SetActive(false);
        Employee.SetActive(true);
        Inputtext.SetActive(true);
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

        Inputtext.SetActive(true);
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

        Inputtext.SetActive(false);
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
        if (Scene_Link2.Instance.tankantenna == true)
        {

            easythrow.interactable = true;
            
        }
        else
        {
            easythrow.isOn = false;
            easythrow.interactable = false;
            Scene_Link2.Instance.easythrow = false;
        }
        Throw(easythrow);

    }
    void Throw(bool i)
    {
        {
            if(i==true)
            {
                Scene_Link2.Instance.easythrow = true;
            }
            else
            {
                Scene_Link2.Instance.easythrow = false;
            }
        }
    }
}
