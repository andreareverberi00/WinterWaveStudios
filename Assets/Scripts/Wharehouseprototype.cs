using UnityEngine;

public class Wharehouseprototype : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        WastePool.Instance.ReturnWaste(gameObject);
    }
}
