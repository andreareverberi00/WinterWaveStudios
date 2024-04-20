using System.Collections.Generic;
using UnityEngine;

public class MovementCB : MonoBehaviour
{
    [SerializeField]
    public float speed, conveyorSpeed, initialspeed, ac,initialcb,cb;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private List<GameObject> onBelt;
    [SerializeField]
    public bool accelaration;
    [SerializeField]
    private Material material;
    [SerializeField]
    private float elapsedTime = 0f;
    [SerializeField]
    public float increaseSpeedInterval = 10;
    [SerializeField]
    public float accelerationspeed = 0;
    [SerializeField]
    //public bool isMoved=false;

    // Start is called before the first frame update
    void Start()
    {
        /* Create an instance of this texture
         * This should only be necessary if the belts are using the same material and are moving different speeds
         */
        material = GetComponent<MeshRenderer>().material;
        initialspeed = speed;
        ac = speed * 2;
        initialcb = conveyorSpeed;
        cb = conveyorSpeed * 2;
        //accelaration = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // Move the conveyor belt texture to make it look like it's moving
        material.mainTextureOffset += new Vector2(0, 1) * conveyorSpeed * Time.deltaTime;

        if (accelaration == true)
        {
            //AddSpeed();
            AddSpeedSpeaker();
        }
        if(GameController.Instance.slow==true)
        {
            SlowTime();
        }
        else if(GameController.Instance.slow == false)
        {
            NormalTime();
        }
        //Debug.Log(speed);
    }

    // Fixed update for physics
    void FixedUpdate()
    {
        // For every item on the belt, add force to it in the direction given
        for (int i = 0; i <= onBelt.Count - 1; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().velocity = speed * direction;
            //if(isMoved==true)
            //{
            //    onBelt[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            //}
        }
    }

    // When something collides with the belt
    private void OnCollisionEnter(Collision collision)
    {
        //isMoved = false;
        onBelt.Add(collision.gameObject);
        
    }

    // When something leaves the belt
    private void OnCollisionExit(Collision collision)
    {
        //isMoved = true;
        onBelt.Remove(collision.gameObject);

    }
    void AddSpeed()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= increaseSpeedInterval)
        {
            elapsedTime = 0f;
            speed += accelerationspeed;
            conveyorSpeed += 0.065f * accelerationspeed;

        }

    }
    void AddSpeedSpeaker()
    {
        if (SpeakerController.Instance.Speed == true)
        {
            speed = ac;
            conveyorSpeed = cb;
        }
        else
        {
            speed = initialspeed;
            conveyorSpeed = initialcb;
        }
    }
    public void SlowTime()
    {
        if (speed == initialspeed && conveyorSpeed==initialcb)
        {
            Debug.Log("tempo rallentato ");
            speed = speed * 0.5f;
            conveyorSpeed = conveyorSpeed * 0.5f;

    }
        if(speed==accelerationspeed && conveyorSpeed == 0.065f*accelerationspeed)
        {
            speed = speed* 1/(accelerationspeed*2);
            conveyorSpeed = conveyorSpeed* 1 / (accelerationspeed*0.065f* 2);
        }
        else if (speed == ac && conveyorSpeed == cb)
{
    speed = speed * 0.25f;
    conveyorSpeed = conveyorSpeed * 0.25f;
}

    }

    public void NormalTime()
    {
        if(speed!=initialspeed && conveyorSpeed!=initialcb)
        {
            speed = initialspeed;
            conveyorSpeed = initialcb;
        }
    }
}
