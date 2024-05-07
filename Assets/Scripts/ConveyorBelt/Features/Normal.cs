using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : MonoSingleton<Normal>
{
    public float convospeed;
   public void  Checkspeed(float speed ,float conveyorSpeed,float initialcb)
    {
        if (speed >= 1.2f && speed < 1.3f)
        {

            conveyorSpeed = 0.082f;
            convospeed = conveyorSpeed;

        }
        //if (speed >= 1.3f && speed < 1.4f)
        //{
        //    print("speed 1.3");
        //    conveyorSpeed = 0.09f;
        //}
        if (speed >= 1.4f && speed < 1.5f)
        {
            
            conveyorSpeed = 0.096f;
            convospeed = conveyorSpeed;

        }
        //if (speed >= 1.5f && speed < 1.6f)
        //{
        //    conveyorSpeed = 0.105f;
        //}
        if (speed >= 1.6f && speed < 1.7f)
        {
            conveyorSpeed = 0.115f;
            convospeed = conveyorSpeed;
        }
        //if (speed >= 1.7f && speed < 1.8f)
        //{
        //    conveyorSpeed = 0.12f;
        //}
        if (speed >= 1.8f && speed < 1.9f)
        {
            conveyorSpeed = 0.13f;
            convospeed = conveyorSpeed;
        }
        if (speed >= 2f && speed < 2.1f)
        {
            conveyorSpeed = 0.46f;
            convospeed = conveyorSpeed;
        }
        if (speed >= 2.2f && speed < 2.3f)
        {
            conveyorSpeed = 0.165f;
            convospeed = conveyorSpeed;
        }
        if (speed >= 2.4f && speed < 2.5f)
        {
            conveyorSpeed = 0.181f;
            convospeed = conveyorSpeed;
        }
        if (speed >= 2.6f && speed < 2.7f)
        {
            conveyorSpeed = 0.198f;
            convospeed = conveyorSpeed;
        }
        if (speed >= 2.8f && speed < 2.9f)
        {
            conveyorSpeed = 0.2125f;
            convospeed = conveyorSpeed;
        }
        initialcb = conveyorSpeed;
    }
}
