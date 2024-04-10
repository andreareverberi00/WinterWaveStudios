using UnityEngine;

public class GravityBox : MonoBehaviour
{
    private Transform targetPoint;
    public float attractionForce = 10f;

    private void Start()
    {
        Transform firstChild = transform.GetChild(0);
        targetPoint = firstChild;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waste")) 
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (targetPoint.position - other.transform.position).normalized;
                rb.AddForce(direction * attractionForce, ForceMode.Acceleration);
            }
        }
    }
}
