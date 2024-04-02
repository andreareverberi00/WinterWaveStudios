using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltController : MonoSingleton<ConveyorBeltController>
{
    public GameObject Belt;

    void Start()
    {
        
    }

    // Update is called once per frame
    public  void CreateAPiece()
    {

    GameObject CB1 = Instantiate(Belt, new Vector3(-8.63f, 0.37f, -1.53f), Quaternion.Euler(0,90,0));
    GameObject CB2 = Instantiate(Belt, new Vector3(-3.43f, 0.37f, -1.53f), Quaternion.Euler(0, 90, 0));
    GameObject CB3 = Instantiate(Belt, new Vector3(1.76f, 0.37f, -1.53f), Quaternion.Euler(0, 90, 0));
    GameObject CB4= Instantiate(Belt, new Vector3(6.93f, 0.37f, -1.53f), Quaternion.Euler(0, 90, 0));
    GameObject CB5= Instantiate(Belt, new Vector3(12.13f, 0.37f, -1.53f), Quaternion.Euler(0, 90, 0));

        GameObject CB6 = Instantiate(Belt, new Vector3(-8.63f, -0.63f, -1.53f), Quaternion.Euler(0, 90, 0));
        GameObject CB7 = Instantiate(Belt, new Vector3(-3.43f, -0.63f, -1.53f), Quaternion.Euler(0, 90, 0));
        GameObject CB8 = Instantiate(Belt, new Vector3(1.76f, -0.63f, -1.53f), Quaternion.Euler(0, 90, 0));
        GameObject CB9 = Instantiate(Belt, new Vector3(6.93f, -0.63f, -1.53f), Quaternion.Euler(0, 90, 0));
        GameObject CB10 = Instantiate(Belt, new Vector3(12.13f, -0.63f, -1.53f), Quaternion.Euler(0, 90, 0));


    }

    //}

}
