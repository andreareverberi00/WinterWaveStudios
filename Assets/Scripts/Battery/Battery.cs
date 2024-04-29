using UnityEngine;

public class Battery : MonoBehaviour
{
    public int energyAmount = 10;
    public GameObject Portal;
    private void OnMouseDown()
    {
        BatteryController.Instance.CollectBattery(energyAmount);
        WastePool.Instance.ReturnBattery(gameObject);
    }
    private void Update()
    {
        if (transform.position.x >= CameraView.Instance.maxcamera)
        {
            WastePool.Instance.ReturnBattery(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
            WastePool.Instance.ReturnWaste(gameObject);
        if (collision.collider.GetType() == typeof(CapsuleCollider))
        {
            Debug.Log("Collisione con un oggetto che ha un capsule collider");
            transform.position = new Vector3(Portal.transform.position.x + 0.1f, Portal.transform.position.y, Portal.transform.position.z);
        }

    }
}
