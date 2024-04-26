using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public MenùUI Menu;
    public bool isPushed;

    public void Play(string Main)
    {
        // Carica la scena specificata
        isPushed = true;
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
