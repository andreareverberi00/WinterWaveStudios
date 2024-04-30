using UnityEngine;

public class SwipeLauncher : MonoBehaviour
{
    public Rigidbody objectToLaunch;
    public float launchForce = 10f;

    private Vector3 swipeStartPos;
    private Vector3 swipeEndPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swipeStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipeEndPos = Input.mousePosition;
            LaunchObject();
        }
    }

    void LaunchObject()
    {
        Vector2 swipeDirection = swipeEndPos - swipeStartPos;
        Vector3 launchDirection = new Vector3(swipeDirection.x, 0, swipeDirection.y).normalized;
        objectToLaunch.AddForce(launchDirection * launchForce, ForceMode.Impulse);
    }
}