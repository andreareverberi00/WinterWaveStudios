using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltMovement : MonoBehaviour
{
    [Range(1f,5f)]
    public float speed = 1;

    private Rigidbody rb;
    private float elapsedTime = 0f;
    public  float increaseSpeedInterval = 10;
    public float acceleration;
    public float lenght;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        lenght = GetComponent<Renderer>().bounds.size.x;
        //StartCoroutine(MoveObject());
    }

    //IEnumerator MoveObject()
    //{
    //    while (true)
    //    {
    //elapsedTime += Time.deltaTime; 

    //    if (elapsedTime >= increaseSpeedInterval)
    //    {
    //        SpeedUp();
    //elapsedTime = 0f;
    //    }
//        Movement();
//        yield return new WaitForSeconds(0.001f); // Attendi per un secondo tra i movimenti
//    }
//}

    void Update()
    {
        elapsedTime += Time.deltaTime; 

        if (elapsedTime >= increaseSpeedInterval)
        {
            SpeedUp();
            elapsedTime = 0f;
        }
        Movement();     
    }

    void Movement()
    {

         if (transform.position.y >= 0)
        {

            rb.velocity = Vector3.right * speed;
            if (transform.position.x >= Cameraview.Instance.maxcamera +lenght)
            {

                transform.position = new Vector3(Cameraview.Instance.mincamera-lenght/1.65f,transform.position.y,transform.position.z);
                
            }
         }
        else if (transform.position.y <= 0)
        {
            rb.velocity = Vector3.left * speed;
            if (transform.position.x <= Cameraview.Instance.mincamera-lenght)
            {
                transform.position = new Vector3(Cameraview.Instance.maxcamera+lenght/1.65f , transform.position.y, transform.position.z);

            }

        }
    }
    void SpeedUp()
    {
            speed+=acceleration;
    }



}

