using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCB : MonoBehaviour
{
    [SerializeField]
    private  float speed, conveyorSpeed, initialspeed, ac,ac2,initialcb,cb,cb2,texturedir;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private List<GameObject> onBelt;
    [SerializeField]
    public bool accelaration ;
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
        ac = 2.4f;
        ac2 = 3.5f;
        initialcb = conveyorSpeed;
        cb = 0.181f;
        cb2 = 0.27f;
        //accelaration = false;
        InvokeRepeating("Marcia", 10, 40);
    }

    private void Update()
    {

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
        UnifiedSpawner.Instance.speedcheck = speed;
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
              if(speed<2)
            {
                speed = ac;
                conveyorSpeed = cb;

            }

            else if(initialspeed > 2)
            {
                speed = ac2;
                conveyorSpeed = cb2;
            }

        }
            else
            {
                speed = initialspeed;
                conveyorSpeed = initialcb;
            }
    
    }
    public void SlowTime()
    {
        if (speed == initialspeed && conveyorSpeed == initialcb)
        {
            Debug.Log("tempo rallentato ");
            Deaccelaration.Instance.Slowspeed(speed, conveyorSpeed);
            speed = Deaccelaration.Instance.slow;
            conveyorSpeed = Deaccelaration.Instance.slowcb;
            ////if(SpeakerController.Instance.Speed==false)
            ////{
            //speed = 0.6f;
            //conveyorSpeed = 0.03f;
            ////
            ////
        }

        else if (speed == ac && conveyorSpeed == cb)
        {
            speed = initialspeed;
            conveyorSpeed = initialcb;
        }
        else
        {
            speed = initialspeed;
            conveyorSpeed = initialcb;
        }
    }

    void Marcia()
    {
        if (speed <= 2.8f)
        {
            speed = speed + 0.2f;
            initialspeed = speed;
            Normal.Instance.Checkspeed(speed, conveyorSpeed, initialcb);
            conveyorSpeed = Normal.Instance.convospeed;
            initialcb = conveyorSpeed;

        }
        Normal.Instance.Checkspeed(speed, conveyorSpeed, initialcb);


    }
 
}
