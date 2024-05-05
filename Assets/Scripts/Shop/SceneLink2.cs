using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Link2 : MonoSingleton<Scene_Link2>
{
    public bool easythrow=false;
    public bool baseantenna;
    public bool tankantenna;
    public bool proantenna;
    public bool perkpaper;
    public bool perkglass;
    public bool perkplastic;
    public bool perkorganic;
    public bool perkmetal;
    void Start()
    {

        PlayerPrefs.SetInt("baseantenna", baseantenna ? 1 : 0);
        baseantenna = PlayerPrefs.GetInt("baseantenna", 0) == 1;
        PlayerPrefs.SetInt("tankantenna", tankantenna ? 1 : 0);
        tankantenna = PlayerPrefs.GetInt("tankantenna", 0) == 1;
        PlayerPrefs.SetInt("proantenna", proantenna ? 1 : 0);
        proantenna = PlayerPrefs.GetInt("proantenna", 0) == 1;
        PlayerPrefs.SetInt("perkpaper", perkpaper ? 1 : 0);
        perkpaper = PlayerPrefs.GetInt("perkpaper", 0) == 1;
        PlayerPrefs.SetInt("perkglass", perkglass ? 1 : 0);
        perkglass = PlayerPrefs.GetInt("perkglass", 0) == 1;
        PlayerPrefs.SetInt("perkplastic", perkplastic ? 1 : 0);
        perkplastic = PlayerPrefs.GetInt("perkplastic", 0) == 1;
        PlayerPrefs.SetInt("perkorganic", perkorganic ? 1 : 0);
        perkorganic = PlayerPrefs.GetInt("perkorganic", 0) == 1;
        PlayerPrefs.SetInt("perkmetal", perkmetal ? 1 : 0);
        perkmetal = PlayerPrefs.GetInt("perkmetal", 0) == 1;


    }
    //private void Update()
    //{
    //    Debug.Log(ScoreController.Instance.scoreAmount);
    //}
}
