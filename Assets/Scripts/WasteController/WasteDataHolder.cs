using UnityEngine;

public class WasteDataHolder : MonoBehaviour
{
    public Waste wasteData;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bin"))
        {
            BinDataHolder binHolder = other.GetComponent<BinDataHolder>();
            if (binHolder != null && wasteData != null)
            {
                bool isCorrectSorting = wasteData.wasteType == binHolder.binData.acceptsType;
                WastePool.Instance.ReturnWaste(gameObject);

                if (isCorrectSorting)
                {
                    Debug.Log("Correct sorting!");
                    ScoreController.Instance.AddScore();
                }
                else
                {
                    Debug.Log("Incorrect sorting.");
                    //ScoreController.Instance.RemoveScore();
                    BatteryController.Instance.ConsumeEnergy();
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Floor"))
                WastePool.Instance.ReturnWaste(gameObject);

    }
    private void Update()
    {
        if (transform.position.x >= Cameraview.Instance.maxcamera)
        {
            WastePool.Instance.ReturnWaste(gameObject);
        }
    }
}
