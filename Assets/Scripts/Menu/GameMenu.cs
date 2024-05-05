using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMenu : MonoBehaviour
{
    public MenùUI Menu;
    private bool play;
    private bool shop;


    void Start()
    {
        play = false;
        shop = false;
    }


    public void Play()
    {
        play=true;
        //StartCoroutine(WaitandLoad());
        
        
    }
    public void Custom()
    {
        shop = true;
        StartCoroutine(WaitandLoad2());
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
    public void Quit() 
    {
        Application.Quit();
    }

    IEnumerator WaitandLoad()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Main");
        
        
    }
    IEnumerator WaitandLoad2()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("CustomizationMenu");


    }
    private void Update()
    {
        if(play==true && shop==false)
        {
            
            StartCoroutine(WaitandLoad());
            play = false;
        }
        if (play == false && shop == true)
        {
            StartCoroutine(WaitandLoad2());
            shop = false;
        }
        
    }
}
