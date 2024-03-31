using UnityEngine;

public class Battery : MonoBehaviour
{
    public int energyAmount = 10; 

    private void OnMouseDown()
    {
        BatteryController.Instance.CollectBattery(energyAmount);
        WastePool.Instance.ReturnWaste(gameObject);
    }
    private void Update()
    {
        if (transform.position.x >= CameraView.Instance.maxcamera)
        {
            WastePool.Instance.ReturnWaste(gameObject);
        }
    }
}
