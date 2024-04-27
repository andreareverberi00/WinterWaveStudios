using System.Collections;
using UnityEngine;

public class ConveyorBeltController : MonoSingleton<ConveyorBeltController>
{
    public GameObject Belt;
    public GameObject Belt2;
    public GameObject Wastepoint;
    public GameObject Wastepoint2;
    public bool anotherspawn = false;
    public int secondtoActive = 10;
    private bool isObjectSpawned = false;

    void Start()
    {

        Belt.SetActive(true);
        Belt2.SetActive(false);
        Wastepoint.SetActive(true);
        Wastepoint2.SetActive(false);
    }

    IEnumerator ActiveObject(float secondtoActive)
    {
        yield return new WaitForSeconds(secondtoActive);
        Belt2.SetActive(true);
        Wastepoint2.SetActive(true);
        anotherspawn = true;
    }
    public void ActiveObj()
    {
        StartCoroutine(ActiveObject(secondtoActive));
    }
}
