using UnityEngine;

public class ThrowController : MonoBehaviour
{
    private GameObject selectedWaste;
    Rigidbody rb;

    private float startTime, endTime, swipeTime;
    private Vector2 lastMousePosition;

    [SerializeField]
    private float throwSpeed = 35f;
    private float speed;

    private bool thrown, holding;

    private Vector3 startPosition;
    private Quaternion startRotation;

    public LayerMask selectableLayerMask;
    public Vector3 force = new Vector3(1,1,1);
    public Vector3 maxForce = new Vector3(10, 10, 10);
    private bool alreadyHighlighted = false;
    void SetupWaste(GameObject selectedWaste)
    {
        this.selectedWaste = selectedWaste;
        rb = this.selectedWaste.GetComponent<Rigidbody>();
        startPosition = this.selectedWaste.transform.position;
        startRotation = this.selectedWaste.transform.rotation;
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = Vector3.zero;
    }
    void ResetProperties()
    {
        lastMousePosition = Vector2.zero;

        startTime = 0;
        endTime = 0;
        swipeTime = 0;

        thrown = holding = false;
    }

    void ResetSelectedWasteProperties()
    {
        selectedWaste.transform.position = startPosition;
        selectedWaste.transform.rotation = startRotation;
        selectedWaste = null;
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    void PickupBall()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane * 10f;
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePos);
        selectedWaste.transform.position = newPosition;
        selectedWaste.transform.rotation = startRotation;

         //Vector3.Lerp(selectedWaste.transform.position, newPosition, 80f * Time.deltaTime);
    }

    void HighlightCorrectBin() {  
        if (selectedWaste != null)
        {
            Vector3? binPosition = WasteController.Instance.GetBinPositionForWasteType(selectedWaste.GetComponent<WasteDataHolder>());
            if (binPosition.HasValue&&!alreadyHighlighted)
            {
                print("Highlighting bin");
                VFXController.Instance.PlayVFXAtPosition(VFXType.Sparkles, binPosition.Value, 2f);
                alreadyHighlighted = true;
            }
        }
    }
    void StopHighlightCorrectBin() 
    {
        VFXController.Instance.PlayVFXAtPosition(VFXType.Sparkles, new Vector3(100,0,0), 0f);
    }
    private void Update()
    {
        if (holding && selectedWaste)
        {
            PickupBall();
            HighlightCorrectBin();
        }
        else
        {
            alreadyHighlighted= false;
            StopHighlightCorrectBin();
        }

        if (thrown)
            return;
    

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;

            if (Physics.Raycast(ray, out _hit, 50f, selectableLayerMask))
            {
                SetupWaste(_hit.transform.gameObject);

                if (_hit.transform == selectedWaste.transform)
                {
                    startTime = Time.time;
                    holding = true;
                }
            }
        }

        if (holding && Input.GetMouseButtonUp(0))
        {
            endTime = Time.time;
            swipeTime = endTime - startTime;

            if (swipeTime < 0.8f && lastMousePosition.y < Input.mousePosition.y)
            {
                CalculateAndApplyForce(Input.mousePosition);
            }
            else
            {
                ResetSelectedWasteProperties();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }
    }

    private void CalculateAndApplyForce(Vector2 mousePos)
    {
        float differenceY = (mousePos.y - lastMousePosition.y) / Screen.height * 100;

        if(differenceY>maxForce.y)
            differenceY = maxForce.y;

        speed = throwSpeed * differenceY;

        float x = (mousePos.x / Screen.width) - (lastMousePosition.x / Screen.width);
        x = Mathf.Abs(Input.mousePosition.x - lastMousePosition.x) / Screen.width * 100 * x;

        // if x>maxForce.x, x = maxForce.x
        if (x > maxForce.x)
            x = maxForce.x;

        Vector3 direction = new Vector3(x*force.x, 0f, force.z);
        direction = Camera.main.transform.TransformDirection(direction);

        Debug.DrawLine(selectedWaste.transform.position, selectedWaste.transform.position + direction, Color.red, 2f);

        rb.AddForce((direction * speed / 2f) + (Vector3.up * speed*force.y));

        holding = false;
        thrown = true;

        selectedWaste.GetComponent<WasteDataHolder>().StartCoroutine("ReturnWaste");

        ResetProperties();
    }

}