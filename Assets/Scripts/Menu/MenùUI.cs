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
    public GameObject musicButton;
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
        musicButton.GetComponentInChildren<TMP_Text>().text = PlayerPrefs.GetInt("MusicEnabled", 1) == 1 ? "MUSIC: ON" : "MUSIC: OFF";
        AudioListener.pause = PlayerPrefs.GetInt("MusicEnabled", 1) == 1 ? false : true;
        easythrow.isOn = false;
    }
    public void OnMusicButtonPressed()
    {
        ToggleMusic();
    }

    private void ToggleMusic()
    {
        // Leggi lo stato corrente della musica dalle PlayerPrefs (ritorna 0 se non definito)
        bool isEnabled = PlayerPrefs.GetInt("MusicEnabled") == 1;
        print(isEnabled);
        // Cambia lo stato della musica
        isEnabled = !isEnabled;

        // Imposta il volume dell'audio listener
        AudioListener.pause = isEnabled ? false : true;

        // Salva il nuovo stato nelle PlayerPrefs
        PlayerPrefs.SetInt("MusicEnabled", isEnabled ? 1 : 0);

        // Aggiorna il testo del bottone
        musicButton.GetComponentInChildren<TMP_Text>().text = "MUSIC: " + (isEnabled ? "ON" : "OFF");
        PlayerPrefs.Save();
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
            Scene_Link2.Instance.easythrow = true;

        }
        else
        {
            easythrow.isOn = false;
            easythrow.interactable = false;
            Scene_Link2.Instance.easythrow = false;
        }
        //Throw(easythrow);

    }

}
