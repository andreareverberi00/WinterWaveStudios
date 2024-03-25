using UnityEngine;

public class WasteDataHolder : MonoBehaviour
{
    public Waste wasteData;

    void OnTriggerEnter(Collider other)
    {
        // Controlla se l'oggetto che ha innescato il trigger è un Bin
        if (other.CompareTag("Bin"))
        {
            // Ottieni il BinDataHolder dall'oggetto con cui hai colliso
            BinDataHolder binHolder = other.GetComponent<BinDataHolder>();
            if (binHolder != null && wasteData != null)
            {
                // Controlla se il tipo di rifiuto corrisponde al tipo accettato dal Bin
                if (wasteData.wasteType == binHolder.binData.acceptsType)
                {
                    Debug.Log("Correct sorting!");
                }
                else
                {
                    Debug.Log("Incorrect sorting.");
                }
                // Indipendentemente dal risultato, il rifiuto viene restituito al pool
                WastePool.Instance.ReturnWaste(gameObject);
            }
        }
    }
    private void Update()
    {
        if (transform.position.x >= Cameraview.Instance.maxcamera)
        {
            WastePool.Instance.ReturnWaste(gameObject);
        }
    }
}
