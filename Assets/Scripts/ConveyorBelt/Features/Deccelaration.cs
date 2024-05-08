using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deaccelaration : MonoSingleton<Deaccelaration>
{
    public float slow;
    public float slowcb;
    public void Slowspeed(float speed, float conveyorSpeed)
    {
  
        if(speed>=1.2 && speed<1.3f)
        {
            slow = 0.36f;
            slowcb = 0.0105f;
            speed = slow;
            conveyorSpeed = slowcb;
        }
        if (speed >= 1.4 && speed < 1.5f)
        {
            slow = 0.42f;
            slowcb = 0.015f;
            speed = slow;
            conveyorSpeed = slowcb;
        }
        if (speed >= 1.6 && speed < 1.7f)
        {
            slow = 0.48f;
            slowcb = 0.0205f;
            speed = slow;
            conveyorSpeed = slowcb;
        }
        if (speed >= 1.8 && speed < 1.9f)
        {
            slow = 0.54f;
            slowcb = 0.0255f;
            speed = slow;
            conveyorSpeed = slowcb;
        }
        if (speed >= 2 && speed < 2.1f)
        {
            slow = 0.6f;
            slowcb = 0.03f;
            speed = slow;
            conveyorSpeed = slowcb;
        }
        if (speed >= 2.2 && speed < 2.3f)
        {
            slow = 0.66f;
            slowcb = 0.0354f;
            speed = slow;
            conveyorSpeed = slowcb;
        }
        if (speed >= 2.4 && speed < 2.5f)
        {
            slow = 0.72f;
            slowcb = 0.041f;
            speed = slow;
            conveyorSpeed = slowcb;

        }
        if (speed >= 2.6 && speed < 2.7f)
        {
            slow = 0.78f;
            slowcb = 0.0456f;
            speed = slow;
            conveyorSpeed = slowcb;
        }
        if (speed >= 2.8 && speed < 2.9f)
        {
            slow = 0.84f;
            slowcb = 0.050625f;
            speed = slow;
            conveyorSpeed = slowcb;
        }
    }
}
