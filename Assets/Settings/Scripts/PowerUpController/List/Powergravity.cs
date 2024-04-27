using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powergravity : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject Portal;
    public int AttractionForce=1000;
    private float previousval;
    public float jumpForce = 10;
    public bool isGrounded = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnMouseDown()
    {
        transform.position = new Vector3(100, 100, 100);
        StartCoroutine(MultiplicatorGame());
        previousval = GravityBoxController.Instance.AttractionForce;

    }


    IEnumerator MultiplicatorGame()
    {
        
        GravityBoxController.Instance.AttractionForce = AttractionForce;
        VFXController.Instance.PlayVFXAtPosition(VFXType.PowerUp, GameController.Instance.GetRobotPosition(), 10f);
        yield return new WaitForSeconds(10f); // attendi per 10 secondi
        GravityBoxController.Instance.AttractionForce = previousval; // ripristina la velocità normale del gioco
        PowerPool.Instance.ReturnPower(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
            PowerPool.Instance.ReturnPower(gameObject);
        if (collision.collider.GetType() == typeof(CapsuleCollider))
        {
            //Debug.Log("Collisione con un oggetto che ha un capsule collider");
            transform.position = new Vector3(Portal.transform.position.x - 0.1f, Portal.transform.position.y, Portal.transform.position.z);
        }
        if (collision.collider.GetType() == typeof(BoxCollider))
        {
            isGrounded = true;
            //Jump();
        }

    }
    void Jump()
    {
        if(isGrounded==true)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
    }
}
