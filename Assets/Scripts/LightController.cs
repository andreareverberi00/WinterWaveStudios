using UnityEngine;

public class LightController : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public Light spotlight;
    public Light spotlight2;
    public GameObject Spotlight;
    
    void Start()
    {
        SpeakerController.Instance.alreadyplayed = false; //spotlight = GetComponent<Light>(); // Ottieni il componente Light
    }

    void Update()
    {
        // Ruota la luce attorno all'asse Y
        spotlight.transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
        spotlight2.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        if (SpeakerController.Instance.Speed == true)
        {
            Spotlight.SetActive(true);
            if(SpeakerController.Instance.alreadyplayed == false)
            {
                int i;
                i = Random.Range(0, 2);
                OvertimeSound(i);
            }
         

        }
        else
        {
            Spotlight.SetActive(false);
        }
    }

    void OvertimeSound(int i)
    {
        if (i == 1)
        {
            SpeakerSpeakController.Instance.PlaySound("overtime_1");
            SpeakerController.Instance.alreadyplayed = true;
        }
        if (i == 2)
        {
            SpeakerSpeakController.Instance.PlaySound("overtime_2");
            SpeakerController.Instance.alreadyplayed = true;
        }
    }
}
