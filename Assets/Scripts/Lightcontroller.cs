using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightcontroller : MonoBehaviour
{
    public Light luceRuotante;
    public Light luceRuotante2;
    public GameObject Speaker;
    public float velocitaRotazione = 30f;


    void Update()
    {
        // Ruota la luce attorno all'asse Y
        luceRuotante.transform.Rotate(Vector3.right, velocitaRotazione * Time.deltaTime);
        luceRuotante2.transform.Rotate(Vector3.left, velocitaRotazione * Time.deltaTime);
        if (SpeakerController.Instance.Speed == true)
        {
            Speaker.SetActive(true);
        }
        else
        {
            Speaker.SetActive(false);
        }
    }
}
