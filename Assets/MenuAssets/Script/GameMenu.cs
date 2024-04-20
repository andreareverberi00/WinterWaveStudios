using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public MenùUI Menu;

    public void Play(string Main)
    {

            // Carica la scena specificata
            SceneManager.LoadScene(Main);
        
    }
    public void Custom( string Shop)
    {
        SceneManager.LoadScene(Shop);
    }
    public void IDK()
    {

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
