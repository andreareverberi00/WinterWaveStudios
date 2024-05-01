using UnityEngine;

public class ThrowController : MonoSingleton<ThrowController>
{
    GameObject selectedWaste;
    Rigidbody rb;

    float startTime, endTime, swipeTime;

    [SerializeField]
    float dragTimer = 0f, speed=5f;

    Vector2 startMousePosition;

    bool thrown, holding;

    Vector3 startPosition;
    Quaternion startRotation;

    public LayerMask selectableLayerMask;

    public Vector3 force = new Vector3(1,1,1);

    bool alreadyHighlighted = false;

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
        startMousePosition = Vector2.zero;

        startTime = 0;
        endTime = 0;
        swipeTime = 0;
        dragTimer = 0;

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
        // Se sto tenendo un rifiuto e non è null
        if (holding && selectedWaste)
        {
            print(rb.velocity);
            dragTimer += Time.deltaTime;
            PickupBall();
            HighlightCorrectBin();
        }
        else
        {
            dragTimer = 0f;
            alreadyHighlighted= false;
            StopHighlightCorrectBin();
        }
        // Se ho già lanciato il rifiuto non faccio niente
        if (thrown)
            return;
    
        // Se clicco
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;

            // Se colpisco un oggetto selezionabile
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
        // Se sto tenendo il rifiuto e rilascio il mouse
        if (holding && Input.GetMouseButtonUp(0))
        {
            endTime = Time.time;
            swipeTime = endTime - startTime;

            // Se il tempo di swipe è minore di 0.8 secondi e il movimento del mouse è verso l'alto
            if (swipeTime < 0.8f && startMousePosition.y < Input.mousePosition.y)
            {
                LaunchObject(Input.mousePosition);
            }
            else
            {
                ResetSelectedWasteProperties();
                ResetProperties();
            }
        }
        // Se sto tenendo il rifiuto e muovo il mouse
        if (Input.GetMouseButton(0))
        {
            // Se ci ho messo troppo a lanciare il rifiuto aggiorno la posizione del mouse
            if (dragTimer>0.9f)
            {
                startMousePosition = Input.mousePosition;
                dragTimer = 0f;
            }
        }
    }
    void LaunchObject(Vector2 lastMousePos)
    {

        Vector2 swipeDirection = lastMousePos - startMousePosition;

        print("Swipe direction: " + swipeDirection);

        Vector3 launchDirection = new Vector3(swipeDirection.x, 0, swipeDirection.y).normalized;

        rb.AddForce(launchDirection * speed + force, ForceMode.Impulse);

        holding = false;
        thrown = true;

        selectedWaste.GetComponent<WasteDataHolder>().StartCoroutine("ReturnWaste");
        ResetProperties();

    }
}