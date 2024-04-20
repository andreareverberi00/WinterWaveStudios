using UnityEngine;

public class ConveyorBeltMovement : MonoBehaviour
{
    [Range(1f, 5f)]
    public float speed = 1;

    [Range(5f, 6f)]
    public float maxSpeed = 6f;

    private Rigidbody rb;
    private float elapsedTime = 0f;
    public float increaseSpeedInterval = 10;
    public float acceleration;
    public float lenght;
    Vector3 startposition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lenght = GetComponent<Renderer>().bounds.size.x;
        startposition = transform.position;
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
        //Debug.Log(transform.position.z);
    }

    void Movement()
    {

        if (transform.position.y >= 0)
        {

            rb.velocity = Vector3.right * speed;
            if (transform.position.x >= CameraView.Instance.maxcamera + lenght)
            {

                transform.position = new Vector3(CameraView.Instance.mincamera - lenght / 2.3f, transform.position.y, startposition.z);

            }
        }
        else if (transform.position.y <= 0)
        {
            rb.velocity = Vector3.left * speed;
            if (transform.position.x <= CameraView.Instance.mincamera - lenght)
            {
                transform.position = new Vector3(CameraView.Instance.maxcamera + lenght / 2.3f, transform.position.y, startposition.z);

            }

        }
    }
    void SpeedUp()
    {
        if (speed < maxSpeed)
            speed += acceleration;
    }



}

