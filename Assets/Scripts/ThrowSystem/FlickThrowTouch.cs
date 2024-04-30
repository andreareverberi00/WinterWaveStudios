using UnityEngine;
using System.Collections;
//demand an adio source be placed on this component for use with the audio clip
//[RequireComponent(typeof(AudioSource))]
public class FlickThrowTouch : MonoBehaviour
{

    public Vector2 touchStart;
    public Vector2 touchEnd;
    public int flickTime = 5;
    public int flickLength = 0;
    public float ballVelocity = 0.0f;
    public float ballSpeed = 0;
    public Vector3 worldAngle;
    public GameObject ballPrefab;
    float swipeDist;

    private bool GetVelocity = false;
    //public GameObject[] woosh; //no
    //    public AudioClip ballAudio;  //yes
    public float comfortZone = 0.0f;
    public bool couldBeSwipe;
    public float startCountdownLength = 0.0f;
    public bool startTheTimer = false;
    static bool globalGameStart = false;
    static bool shootEnable = false;
    private float startGameTimer = 0.0f;

    private AudioSource asParamsControl;

    void Start()
    {
        //asParamsControl = this.gameObject.GetComponentInChildren<AudioSource>();
        //    this.asParamsControl.playOnAwake = false;
        //    this.asParamsControl.loop = false;
        startTheTimer = true;
        Time.timeScale = 1;
        if (Application.isEditor)
        {
            Time.fixedDeltaTime = 0.01f;
        }
    }

    void Update()
    {
        if (startTheTimer)
        {
            startGameTimer += Time.deltaTime;
        }
        if (startGameTimer > startCountdownLength)
        {
            globalGameStart = true;
            shootEnable = true;
            startTheTimer = false;
            startGameTimer = 0;
        }

        if (shootEnable)
        {
            Debug.Log("enabled!");


            if (Input.GetMouseButtonDown(0))
            {
                flickTime = 5;
                timeIncrease();
                couldBeSwipe = true;
                GetVelocity = true;
                touchStart = Input.mousePosition;
            }

            if (Input.GetMouseButton(0)) 
            {
                if (Mathf.Abs(Input.mousePosition.y - touchStart.y) < comfortZone)
                {
                    couldBeSwipe = false;
                }
                else
                {
                    couldBeSwipe = true;
                }
            }
            /*case TouchPhase.Stationary:
                if (Mathf.Abs(touch.position.y - touchStart.y) < comfortZone)
                {
                    couldBeSwipe = false;
                }
                break;*/

            if (Input.GetMouseButtonUp(0))
            {
                float swipeDist = ((Vector2)Input.mousePosition - touchStart).magnitude;
                //couldBeSwipe
                if (couldBeSwipe || swipeDist > comfortZone)
                {
                    GetVelocity = false;
                    touchEnd = Input.mousePosition;
                    GameObject ball = Instantiate(ballPrefab, new Vector3(0.0f, 2.5f, -3.0f), Quaternion.identity) as GameObject;
                    GetSpeed();
                    GetAngle();
                    ball.GetComponent<Rigidbody>().AddForce(new Vector3((worldAngle.x * ballSpeed), (worldAngle.y * ballSpeed), (worldAngle.z * ballSpeed)));

                }
            }

                if (GetVelocity)
                {
                    flickTime++;
                }
            
        }
        if (!shootEnable)
        {
            Debug.Log("shot disabled!");
        }
    }

    void timeIncrease()
    {
        if (GetVelocity)
        {
            flickTime++;
        }
    }

    void GetSpeed()
    {
        //flickLength = 90;
        if (flickTime > 0)
        {
            ballVelocity = flickLength / (flickLength - flickTime);
        }
        ballSpeed = ballVelocity * 30;
        ballSpeed = ballSpeed - (ballSpeed * 1.65f);
        if (ballSpeed <= -33)
        {
            ballSpeed = -33;
        }
        Debug.Log("flick was" + flickTime);
        flickTime = 5;
    }

    void GetAngle()
    {
        worldAngle = Camera.main.ScreenToWorldPoint(new Vector3(touchEnd.x, touchEnd.y + 50f, ((Camera.main.nearClipPlane - 50.0f))));
    }
}