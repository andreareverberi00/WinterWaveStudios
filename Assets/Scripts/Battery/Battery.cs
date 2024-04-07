using UnityEngine;

public class Battery : MonoBehaviour
{
    public int energyAmount = 10; 

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
}
