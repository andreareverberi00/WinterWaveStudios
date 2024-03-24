using UnityEngine;

public class WasteDataHolder : MonoBehaviour
{
    public Waste wasteData;
    private void Update()
    {
        if(transform.position.x>=Cameraview.Instance.maxcamera)
        {
            Destroy(gameObject);
            Debug.Log("Distrutto");
        }

        
    }
}
