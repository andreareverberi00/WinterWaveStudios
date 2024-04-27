using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public MenùUI Menu;
    public bool nocustom;

    void Start()
    {
        // Carica il valore bool salvato se esiste, altrimenti imposta il valore di default a true
        nocustom = PlayerPrefs.GetInt("nocustom", 1) == 1;
    }


    public void Play(string Main)
    {
        // Carica la scena specificata
        nocustom = !nocustom;
        PlayerPrefs.SetInt("valoreBool", nocustom ? 1 : 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(Main);
    }
    public void Custom( string Shop)
    {
        SceneManager.LoadScene(Shop);
    }
    public void Options()
    {
        Menu.OPTIONS();
    }
    public void Credits()
    {
        Menu.CREDITS();
    }
    public void ReturnToMenu()
    {
        Menu.BackToMenu();
    }
}
