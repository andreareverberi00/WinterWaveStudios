using System.Collections;
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



    void Start()
    {

        material = GetComponent<MeshRenderer>().material;
        initialspeed = speed;
        ac = speed*2;
        initialcb = conveyorSpeed;
        cb = 0.181f;
        //accelaration = false;
        InvokeRepeating("Marcia", 1, 4);
    }

    private void Update()
    {
        // Move the conveyor belt texture to make it look like it's moving
        material.mainTextureOffset += new Vector2(0, texturedir) * conveyorSpeed * Time.deltaTime;
        //textureOffset += speed * textureSpeedMultiplier * Time.deltaTime;
        //material.mainTextureOffset = new Vector2(0, textureOffset);

        if (accelaration == true)
        {
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
    }

    void FixedUpdate()
    {
        // For every item on the belt, add force to it in the direction given
        for (int i = 0; i <= onBelt.Count - 1; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().velocity = speed * direction;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
        
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
        collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    IEnumerator StartMarcia()
    {
        Marcia();
        return null;
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
            conveyorSpeed = 0.0325f;

        }
        else if (speed == ac && conveyorSpeed == cb)
        {
            speed = initialspeed;
            conveyorSpeed = initialcb;
        }

    }

    void Marcia()
    {
        if(speed<=1.8f)
        {
            speed = speed + 0.1f;
            initialspeed = speed;
            Checkspeed();
        }
        Checkspeed();

    }
    void Checkspeed()
    {

        if (speed >= 1.3f&&speed<1.4f)
        {
            print("speed 1.3");
            conveyorSpeed = 0.09f;
        }
        if (speed >= 1.4f && speed < 1.5f)
        {
            conveyorSpeed = 0.096f;
        }
        if (speed >= 1.5f && speed < 1.6f)
        {
            conveyorSpeed = 0.105f;
        }
        if (speed >= 1.6f && speed < 1.7f)
        {
            conveyorSpeed = 0.115f;
        }
        if (speed >= 1.7f && speed < 1.8f)
        {
            conveyorSpeed = 0.12f;
        }
        if (speed >= 1.8f && speed < 1.9f)
        {
            conveyorSpeed = 0.13f;
        }
        initialcb = conveyorSpeed;

    }
}
