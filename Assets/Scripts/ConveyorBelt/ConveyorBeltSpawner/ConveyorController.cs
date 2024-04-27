using System.Collections;
using UnityEngine;

public class ConveyorController : MonoBehaviour
{

    public GameObject Belt;
    public GameObject W;
    public Vector3 position;
    int z = 0;
    void Start()
    {
        StartCoroutine(SpawnObjectEvery10Seconds());
        Instantiate(Belt, transform.position, Quaternion.identity);
        Instantiate(W, transform.position, Quaternion.identity);
        position = new Vector3(0, 0, z);
    }

    // Update is called once per frame
    IEnumerator SpawnObjectEvery10Seconds()
    {
        while (true)
        {
            Instantiate(Belt, position, Quaternion.identity);
            Instantiate(W, position, Quaternion.identity);
            position = new Vector3(0, 0, z + 2);
            yield return new WaitForSeconds(10f);
        }
    }

    //}

}
