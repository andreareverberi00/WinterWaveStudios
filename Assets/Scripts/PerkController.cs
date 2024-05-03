using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkController : MonoSingleton<PerkController>
{
    public bool nocustom=true;
    public GameObject TD;
    public GameObject TE;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // Memorizza l'oggetto tra le scene
    }

    private void Start()
    {
                if (nocustom == false) //Gamemenu.Instance.nocustom==false)
                {
                    Antenna();
                }
            
        
  
        
       
        
    }
    void Antenna()
    {
        if (Scene_Link2.Instance.proantenna == true)
        {
            ScoreController.Instance.scoreAmount = 30;
            BatteryController.Instance.consumeEnergyAmount = 20;
        }
        if (Scene_Link2.Instance.baseantenna == true)
        {
            ScoreController.Instance.scoreAmount = 10;
            BatteryController.Instance.consumeEnergyAmount = 5;
            if(Scene_Link2.Instance.easythrow==true)
            {
                TE.SetActive(true);
                TD.SetActive(false);
            }
            else
            {
                TD.SetActive(true);
            }
        }
        if (Scene_Link2.Instance.tankantenna == true)
        {
            ScoreController.Instance.scoreAmount = 3;
            BatteryController.Instance.consumeEnergyAmount = 1;
        }

    }
   
}
