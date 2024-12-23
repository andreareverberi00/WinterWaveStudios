using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkController : MonoSingleton<PerkController>
{

    bool already;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // Memorizza l'oggetto tra le scene
    }

    private void Update()
    {
      
        Antenna();
        


        if (BatteryController.Instance.currentEnergy>=50&&already)
            already = false;
    }
    void Antenna()
    {
        if (Scene_Link2.Instance.proantenna == true)
        {
            ScoreController.Instance.punteggio = 20;
            BatteryController.Instance.consumeEnergyAmount = 20;
            if (BatteryController.Instance.currentEnergy <= 20 && !already)
            {
                int i;
                i = Random.Range(0, 4);
                Killshot(i);
            }
        }
        if (Scene_Link2.Instance.baseantenna == true)
        {
            ScoreController.Instance.punteggio = 10;
            BatteryController.Instance.consumeEnergyAmount = 10;
            if (BatteryController.Instance.currentEnergy <= 15 && !already)
            {
                int i;
                i = Random.Range(0, 4);
                Killshot(i);
            }

        }
        if (Scene_Link2.Instance.tankantenna == true)
        {
            ScoreController.Instance.punteggio = 5;
            BatteryController.Instance.consumeEnergyAmount = 5;
            if (BatteryController.Instance.currentEnergy <= 10 && already == false)
            {
                int i;
                i = Random.Range(0, 4);
                Killshot(i);
                Debug.Log("cambiato");
            }

        }

    }
    void Killshot(int i)
    {
        if (i == 1)
        {
            SpeakerSpeakController.Instance.PlaySound("one shot 1");
            already = true;
        }
        if (i == 2)
        {
            SpeakerSpeakController.Instance.PlaySound("one shot 2");
            already = true;
        }
        if (i == 3)
        {
            SpeakerSpeakController.Instance.PlaySound("one shot 3");
            already=true;
        }
    }
   
}
