using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powergravity : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject Portal;
    public int AttractionForce=1000;
    public float jumpForce = 10;
    public bool isGrounded = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnMouseDown()
    {
        transform.position = new Vector3(100, 100, 100);
        StartCoroutine(MultiplicatorGame()); int i;
        i = Random.Range(0, 4);
        OvertimeSound(i);
        GameController.Instance.PlayMagnetVFXOnAllBins();

    }


    IEnumerator MultiplicatorGame()
    {
        
        GravityBoxController.Instance.AttractionForce = AttractionForce;
        VFXController.Instance.PlayVFXAtPosition(VFXType.PowerUp, GameController.Instance.GetRobotPosition(), 10f);
        yield return new WaitForSeconds(10f); // attendi per 10 secondi
        GravityBoxController.Instance.AttractionForce = GravityBoxController.Instance.Attractionmagnet; // ripristina la velocit� normale del gioco
        PowerPool.Instance.ReturnPower(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
            PowerPool.Instance.ReturnPower(gameObject);

        if (collision.collider.GetType() == typeof(CapsuleCollider))
        {
            transform.position = new Vector3(Portal.transform.position.x + 0.1f, Portal.transform.position.y, Portal.transform.position.z);
        }
        if (collision.collider.GetType() == typeof(BoxCollider))
        {
            isGrounded = true;
        }

    }
    void OvertimeSound(int i)
    {
        if (i == 1)
        {
            SpeakerSpeakController.Instance.PlaySound("magnetize 1");
            SpeakerController.Instance.alreadyplayed = true;
        }
        if (i == 2)
        {
            SpeakerSpeakController.Instance.PlaySound("magnetize 2");
            SpeakerController.Instance.alreadyplayed = true;
        }
        if (i == 3)
        {
            SpeakerSpeakController.Instance.PlaySound("magnetize 3");
            SpeakerController.Instance.alreadyplayed = true;
        }
    }

}
