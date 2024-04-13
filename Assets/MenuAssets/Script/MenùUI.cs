using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenùUI : MonoSingleton<MenùUI>
{
    public GameObject CreditsText;
    public GameObject Credit;
    public GameObject Menu;
    public GameObject Play;
    public GameObject Custom;
    public GameObject IDk;
    void Start()
    {
        CreditsText.SetActive(false);
        Menu.SetActive(false);
        Play.SetActive(true);
        Custom.SetActive(true);
        IDk.SetActive(true);
        Credit.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CREDITS()
    {
        CreditsText.SetActive(true);
        Menu.SetActive(true);
        Play.SetActive(false);
        Custom.SetActive(false);
        IDk.SetActive(false);
        Credit.SetActive(false);
    }

   public void BackToMenu()
    {
        CreditsText.SetActive(false);
        Menu.SetActive(false);
        Play.SetActive(true);
        Custom.SetActive(true);
        IDk.SetActive(true);
        Credit.SetActive(true);
    }

}
