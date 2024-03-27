using UnityEngine;

public class Battery : MonoBehaviour
{
    public int energyAmount = 20; 

    private void OnMouseDown()
    {
        BatteryController.Instance.CollectBattery(energyAmount);
        WastePool.Instance.ReturnWaste(gameObject);
    }
    private void Update()
    {
        if (transform.position.x >= Cameraview.Instance.maxcamera)
        {
            Destroy(gameObject);
        }
    }
}
