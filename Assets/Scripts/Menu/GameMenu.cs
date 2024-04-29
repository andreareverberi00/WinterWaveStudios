using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public MenùUI Menu;
 

    void Start()
    {

    }


    public void Play(string Main)
    {

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
