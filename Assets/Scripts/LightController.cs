using UnityEngine;

public class LightController : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public Light spotlight;
    public Light spotlight2;
    public GameObject Spotlight;
    void Start()
    {
        //spotlight = GetComponent<Light>(); // Ottieni il componente Light
    }

    void Update()
    {
        // Ruota la luce attorno all'asse Y
        spotlight.transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
        spotlight2.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        if (SpeakerController.Instance.Speed == true)
        {
            Spotlight.SetActive(true);
        }
        else
        {
            Spotlight.SetActive(false);
        }
    }

}
