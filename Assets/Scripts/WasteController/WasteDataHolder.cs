using UnityEngine;

public class WasteDataHolder : MonoBehaviour
{
    public Waste wasteData;
    public GameObject Portal;
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
                    ScoreController.Instance.RecordCorrectThrow();
                    ScoreController.Instance.AddScore();
                    BatteryController.Instance.CollectBattery(10);
                }
                else
                {
                    Debug.Log("Incorrect sorting.");
                    ScoreController.Instance.RecordMissedThrow();
                    BatteryController.Instance.ConsumeEnergy();
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
            WastePool.Instance.ReturnWaste(gameObject);
        if (collision.collider.GetType() == typeof(CapsuleCollider))
        {

            transform.position = new Vector3(Portal.transform.position.x + 0.1f, Portal.transform.position.y, Portal.transform.position.z);
        }

    }
    private void Update()
    {
        if (transform.position.x >= CameraView.Instance.maxcamera && transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x - 1, 0.5f, transform.position.z);
        }
        if (transform.position.x >= CameraView.Instance.maxcamera && transform.position.y <= 0)
        {
            WastePool.Instance.ReturnWaste(gameObject);
        }
    }
}
