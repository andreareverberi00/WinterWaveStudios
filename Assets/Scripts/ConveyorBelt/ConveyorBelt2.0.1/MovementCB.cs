using System.Collections.Generic;
using UnityEngine;

public class MovementCB : MonoBehaviour
{
    [SerializeField]
    private  float speed, conveyorSpeed, initialspeed, ac,initialcb,cb,texturedir;
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
    //private float textureSpeedMultiplier = 1f; // Moltiplicatore per calibrare la velocità della texture

    //private float textureOffset = 0f; // Offset corrente della texture

    //public bool isMoved=false;


    // Start is called before the first frame update
    void Start()
    {
        /* Create an instance of this texture
         * This should only be necessary if the belts are using the same material and are moving different speeds
         */
        material = GetComponent<MeshRenderer>().material;
        initialspeed = speed;
        ac = speed*2;
        initialcb = conveyorSpeed;
        cb = 0.181f;
        //accelaration = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // Move the conveyor belt texture to make it look like it's moving
        material.mainTextureOffset += new Vector2(0, texturedir) * conveyorSpeed * Time.deltaTime;
        //textureOffset += speed * textureSpeedMultiplier * Time.deltaTime;
        //material.mainTextureOffset = new Vector2(0, textureOffset);

        if (accelaration == true)
        {
            //AddSpeed();
            AddSpeedSpeaker();
        }
        if (GameController.Instance.slow==true)
        {
            SlowTime();
        }
        else if(GameController.Instance.slow == false)
        {
            AddSpeedSpeaker();
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
        collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    void AddSpeed()
    {
        //elapsedTime += Time.deltaTime;

        //if (elapsedTime >= increaseSpeedInterval)
        //{
        //    elapsedTime = 0f;
        //    speed += 0.025f;
        //    conveyorSpeed += 0.0017083f;
        //    ac = speed * 2;
        //    cb = conveyorSpeed*2;
        //    initialspeed = speed;
        //    initialcb = conveyorSpeed;

        //}

        if (ScoreController.Instance.Score % 500 == 0)
        {
            Marcia();
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
            //speed = speed * 0.5f;
            //conveyorSpeed = conveyorSpeed * 0.5f;
            speed = speed * 0.5f;
            conveyorSpeed = 0.0325f;

        }
        //if(speed==accelerationspeed && conveyorSpeed == 0.065f*accelerationspeed)
        //{
        //    speed = speed* 1/(accelerationspeed*2);
        //    conveyorSpeed = conveyorSpeed* 1 / (accelerationspeed*0.065f* 2);
        //}
        else if (speed == ac && conveyorSpeed == cb)
        {
            speed = initialspeed;
            conveyorSpeed = initialcb;
        }

    }

    void Marcia()
    {
        speed=speed+0.1f;
        initialspeed = speed;
        Checkspeed();
        
    }
    void Checkspeed()
    {
        if (speed == 1.3f)
        {
            conveyorSpeed = 0.09f;
            conveyorSpeed = initialcb;
        }
        if (speed == 1.4f)
        {
            conveyorSpeed = 0.096f;
            conveyorSpeed = initialcb;
        }
        if (speed == 1.5f)
        {
            conveyorSpeed = 0.105f;
            conveyorSpeed = initialcb;
        }
        if (speed == 1.6f)
        {
            conveyorSpeed = 0.115f;
            conveyorSpeed = initialcb;
        }
        if (speed == 1.7f)
        {
            conveyorSpeed = 0.12f;
            conveyorSpeed = initialcb;
        }
        if (speed == 1.8f)
        {
            conveyorSpeed = 0.13f;
            conveyorSpeed = initialcb;
        }
    }
}
