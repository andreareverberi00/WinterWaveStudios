using System.Collections;
using Unity.VisualScripting;
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

    GameObject trailPrefab;
    public float minSwipeDistance=0f;

    void SetupWaste(GameObject selectedWaste)
    {
        this.selectedWaste = selectedWaste;
        trailPrefab=this.selectedWaste.gameObject.transform.GetChild(0).gameObject;
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

        // Disattiva il trail
        trailPrefab.SetActive(false);
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
                
                if (_hit.transform == selectedWaste.transform) // Secondo me da rimuovere, non necessario
                {
                    startMousePosition = Input.mousePosition; // Mancava questo
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
            float swipeDistance = Vector2.Distance(startMousePosition, Input.mousePosition);

            // Se il tempo di swipe è minore di 0.8 secondi e il movimento del mouse è verso l'alto
            if (swipeTime < 0.9f && startMousePosition.y < Input.mousePosition.y&&swipeDistance>minSwipeDistance)
            {
                ActivateTrail();

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
            if (dragTimer>0.3f)
            {
                startMousePosition = Input.mousePosition;
                dragTimer = 0f;
            }
        }
    }
    private void ActivateTrail()
    {
        // Attiva il trail
        trailPrefab.SetActive(true);
        //trailPrefab.transform.localPosition = Vector3.zero;
    }
    void LaunchObject(Vector2 lastMousePos)
    {

        Vector2 swipeDirection = lastMousePos - startMousePosition;

        float swipeLength = swipeDirection.magnitude; // Calcola la lunghezza dello swipe

        // Normalizza la direzione dello swipe e calcola la forza basata sulla lunghezza dello swipe
        Vector3 launchDirection = new Vector3(swipeDirection.x, swipeLength, swipeDirection.y).normalized;

        // Calcola la forza aggiuntiva basata sulla lunghezza dello swipe
        float additionalForceY = swipeLength * 0.001f;
        float additionalForceZ = swipeLength * 0.002f;

        // Applica la forza iniziale più la forza aggiuntiva basata sulla lunghezza dello swipe
        Vector3 forceVector = (launchDirection * speed + new Vector3(0, additionalForceY, additionalForceZ) + force);

        rb.AddForce(forceVector, ForceMode.Impulse);

        Debug.DrawRay(selectedWaste.transform.position, forceVector, Color.red, 2f);

        holding = false;
        thrown = true;

        selectedWaste.GetComponent<WasteDataHolder>().StartCoroutine("ReturnWaste");
        ResetProperties();

        // Disattiva il trail dopo il lancio
        StartCoroutine(DisableTrailAfterDelay());
    }
    IEnumerator DisableTrailAfterDelay()
    {
        yield return new WaitForSeconds(1.5f); // Imposta una durata adeguata per il trail dopo il lancio
        trailPrefab.SetActive(false);
    }
}