using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltMovement : MonoBehaviour
{
    public int speed = 1; 
    private Rigidbody rb;
    private float elapsedTime = 0f;
    private float increaseSpeedInterval = 10; 


    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        if (transform.position.y <= -0.60)
        {
            rb.velocity = Vector3.left * speed;
            if (transform.position.x <= -8)
            {
                transform.position = new Vector3(12.13f, -0.63f, -1.53f);

            }

        }

        else if (transform.position.y >= 0.36)
        {

            rb.velocity = Vector3.right * speed;
            if (transform.position.x >= 13)
            {

                transform.position = new Vector3(-8.63f, 0.37f, -1.53f);
                
            }
        }
    }
    void SpeedUp()
    {

            speed++;

    }



}

