using System.Collections;
using UnityEngine;

public class SpeakerController : MonoSingleton<SpeakerController>
{
    private int seconds;
    public int secondacc = 10;
    public bool Speed = false;
    public bool alreadyplayed;
    public Vector3 GetPosition()
    {
        return transform.GetChild(1).transform.position;
    }
    void Start()
    {
        seconds = Random.Range(30, 41);
        //Debug.Log(seconds);
        StartCoroutine(SpeakerSpeedUp());
    }

    IEnumerator SpeakerSpeedUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(seconds);
            seconds = Random.Range(30, 41) + secondacc;
            //Debug.Log(seconds + "try2");
            IstantSpeed();
        }
    }

    void IstantSpeed()
    {
        StartCoroutine(Istant());
        ScoreController.Instance.AddOvertimePeriod();
    }
    IEnumerator Istant()
    {
        Speed = true;
        yield return new WaitForSeconds(10);
        Speed = false;
        alreadyplayed = false;

    }


}
