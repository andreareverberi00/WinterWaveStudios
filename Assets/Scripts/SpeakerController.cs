using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerController : MonoSingleton <SpeakerController>
{
    private int seconds;
    public int secondacc=10;
    public bool Speed = false;
    void Start()
    {
        seconds = Random.Range(45, 60);
        //Debug.Log(seconds);
        StartCoroutine(SpeakerSpeedUp());
    }

    IEnumerator SpeakerSpeedUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);
            seconds = Random.Range(45, 60) + secondacc;
            //Debug.Log(seconds + "try2");
            IstantSpeed();
        }
    }

    void IstantSpeed()
    {
        StartCoroutine(Istant());
    }
   IEnumerator Istant()
    {
        Speed = true;
        yield return new WaitForSeconds(10);
        Speed = false;
        
    }
   
}
