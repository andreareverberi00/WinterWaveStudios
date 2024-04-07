using UnityEngine;

public class ThrowController : MonoBehaviour
{
    private GameObject selectedWaste;

    private float startTime, endTime, swipeDistance, swipeTime;
    private Vector2 startPos,endPos;

    public float minSwipeDistance = 0;
    private float BallSpeed = 0;
    public float MaxBallSpeed = 350;

    private string selectableObjectTag = "Waste";

    private bool thrown, holding;

    private Vector3 startPosition;
    private Quaternion startRotation;

    Rigidbody rb;

    public float xForceFactor;
    public float yForceFactor = 5;
    public float zForceFactor = 5;
    public LayerMask LayerMask;

    void SetupWaste(GameObject selectedWaste)
    {
        this.selectedWaste = selectedWaste;
        rb = this.selectedWaste.GetComponent<Rigidbody>();
        startPosition = this.selectedWaste.transform.position;
        startRotation = this.selectedWaste.transform.rotation;
    }
    void ResetBall()
    {
        endPos = Vector2.zero;
        startPos = Vector2.zero;

        BallSpeed = 0;
        startTime = 0;
        endTime = 0;
        swipeDistance = 0;
        swipeTime = 0;

        thrown = holding = false;

        rb.velocity = Vector3.zero;

        selectedWaste.transform.position = startPosition;
        selectedWaste.transform.rotation = startRotation;

        selectedWaste = null;
    }

    void PickupBall()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane * 10f;
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePos);
        selectedWaste.transform.position = newPosition;
        selectedWaste.transform.rotation = startRotation;
        //Vector3.Lerp(Ball.transform.position, newPosition, 80f * Time.deltaTime);
    }

    private void Update()
    {
        if (holding)
            PickupBall();

        if (thrown)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;

            if (Physics.Raycast(ray, out _hit, 50f,LayerMask))
            {
                SetupWaste(_hit.transform.gameObject);

                if (_hit.transform == selectedWaste.transform)
                {
                    startTime = Time.time;
                    startPos = Input.mousePosition;
                    holding = true;
                }
            }
        }
        else if (holding&&Input.GetMouseButtonUp(0))
        {
            endTime = Time.time;
            endPos = Input.mousePosition;
            swipeDistance = (endPos - startPos).magnitude;
            swipeTime = endTime - startTime;

            if (swipeTime < 1f && swipeDistance > minSwipeDistance)
            {
                CalculateSpeed();
                CalculateAndApplyForce(); 

                holding = false;
                thrown = true;

                thrown = holding = false;
            }
            else
            {
                ResetBall();
            }
        }
    }

    private void CalculateAndApplyForce()
    {
        Vector3 swipeStartWorld = Camera.main.ScreenToWorldPoint(new Vector3(startPos.x, startPos.y, Camera.main.nearClipPlane+50f));
        Vector3 swipeEndWorld = Camera.main.ScreenToWorldPoint(new Vector3(endPos.x, endPos.y, Camera.main.nearClipPlane+50f));

        Vector3 swipeDirectionWorld = swipeEndWorld - swipeStartWorld;

        float swipeLength = swipeDirectionWorld.magnitude;

        float xForce = swipeDirectionWorld.x * xForceFactor; 
        float yForce = Mathf.Sqrt(swipeLength) * yForceFactor;
        float zForce = swipeLength * zForceFactor;

        Vector3 forceVector = new Vector3(xForce, yForce, zForce);

        forceVector = new Vector3(forceVector.x, forceVector.y, forceVector.z).normalized * BallSpeed;

        Debug.Log("Force vector: " + forceVector);
        Debug.DrawLine(selectedWaste.transform.position, selectedWaste.transform.position + forceVector, Color.red, 2f);

        rb.AddForce(forceVector, ForceMode.Impulse);
    }


    void CalculateSpeed()
    {
        if (swipeTime > 0)
            BallSpeed = swipeDistance / swipeTime;

        if (BallSpeed >= MaxBallSpeed)
        {
            BallSpeed = MaxBallSpeed;
        }
        swipeTime = 0;

    }
}