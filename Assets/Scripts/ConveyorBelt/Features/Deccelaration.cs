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
            slow = 0.6f;
            slowcb = 0.03f;
            speed = slow;
            conveyorSpeed = slowcb;
        }
        if (speed >= 1.4 && speed < 1.5f)
        {
            slow = 0.7f;
            slowcb = 0.0395f;
            speed = slow;
            conveyorSpeed = slowcb;
        }
        if (speed >= 1.6 && speed < 1.7f)
        {
            slow = 0.8f;
            slowcb = 0.048f;
            speed = slow;
            conveyorSpeed = slowcb;
        }
        if (speed >= 1.8 && speed < 1.9f)
        {
            slow = 0.9f;
            slowcb = 0.057f;
            speed = slow;
            conveyorSpeed = slowcb;
        }
        if (speed >= 2 && speed < 2.1f)
        {
            slow = 1f;
            slowcb = 0.065f;
            speed = slow;
            conveyorSpeed = slowcb;
        }
        if (speed >= 2.2 && speed < 2.3f)
        {
            slow = 1.1f;
            slowcb = 0.065f;
            speed = slow;
            conveyorSpeed = slowcb;
        }
        if (speed >= 2.4 && speed < 2.5f)
        {
            slow = 1.2f;
            slowcb = 0.082f;
            speed = slow;
            conveyorSpeed = slowcb;

        }
        if (speed >= 2.6 && speed < 2.7f)
        {
            slow = 1.3f;
            slowcb = 0.089f;
            speed = slow;
            conveyorSpeed = slowcb;
        }
    }
}
