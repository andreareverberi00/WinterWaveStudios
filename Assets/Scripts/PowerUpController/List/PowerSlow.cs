using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSlow : MonoBehaviour
{
    public GameObject Portal;
    private void OnMouseDown()
    {
        transform.position = new Vector3(100, 100, 100);
        StartCoroutine(SlowDownGame());
       
       

    }
    
    
    IEnumerator SlowDownGame()
    {
        
      
        Time.timeScale = 0.5f; 
        yield return new WaitForSeconds(10f);
       
        Time.timeScale = 1f;
        PowerPool.Instance.ReturnPower(gameObject);
 
    }
 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
            PowerPool.Instance.ReturnPower(gameObject);
        if (collision.collider.GetType() == typeof(CapsuleCollider))
        {
            //Debug.Log("Collisione con un oggetto che ha un capsule collider");
            transform.position = new Vector3(Portal.transform.position.x + 0.1f, Portal.transform.position.y, Portal.transform.position.z);
        }

    }

}
