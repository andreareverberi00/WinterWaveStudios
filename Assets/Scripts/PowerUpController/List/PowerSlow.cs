using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSlow : MonoBehaviour
{
    public GameObject Portal;
    public Rigidbody rb;
    public float jumpForce = 10;
    public bool isGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnMouseDown()
    {
        transform.position = new Vector3(100, 100, 100);
        StartCoroutine(SlowDownGame());
        int i;
        i = Random.Range(0, 4);
        OvertimeSound(i);



    }


    IEnumerator SlowDownGame()
    {

        //Time.timeScale = 0.5f;
        GameController.Instance.slow = true;
        VFXController.Instance.PlayVFXAtPosition(VFXType.PowerUp, GameController.Instance.GetRobotPosition(), 10f);
        yield return new WaitForSeconds(10f);
        GameController.Instance.slow = false;
        //Time.timeScale = 1;
        PowerPool.Instance.ReturnPower(gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {

        
            if (collision.collider.CompareTag("Floor"))
            PowerPool.Instance.ReturnPower(gameObject);
        if (collision.collider.GetType() == typeof(CapsuleCollider))
        {
            //Debug.Log("Collisione con un oggetto che ha un capsule collider");
            transform.position = new Vector3(Portal.transform.position.x + 0.1f, Portal.transform.position.y, Portal.transform.position.z);
        }
        if (collision.collider.GetType() == typeof(BoxCollider))
        {
            isGrounded = true;
            //Jump();

        }
                
    }

    void OvertimeSound(int i)
    {
        if (i == 1)
        {
            SpeakerSpeakController.Instance.PlaySound("slow 3 d");
            SpeakerController.Instance.alreadyplayed = true;
        }
        if (i == 2)
        {
            SpeakerSpeakController.Instance.PlaySound("slow 2");
            SpeakerController.Instance.alreadyplayed = true;
        }
        if (i == 3)
        {
            SpeakerSpeakController.Instance.PlaySound("slow 3 d");
            SpeakerController.Instance.alreadyplayed = true;
        }
    }

}
