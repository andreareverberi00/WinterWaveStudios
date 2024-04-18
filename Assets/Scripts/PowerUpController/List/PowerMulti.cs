using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMulti : MonoBehaviour
{
    public GameObject Portal;
    private void OnMouseDown()
    {
        transform.position = new Vector3(100, 100, 100);
        StartCoroutine(MultiplicatorGame());
        PowerPool.Instance.ReturnPower(gameObject);

    }


    IEnumerator MultiplicatorGame()
    {
        
        ScoreController.Instance.scoreAmount=ScoreController.Instance.scoreAmount * 2;// rallenta il gioco al 50%
        yield return new WaitForSeconds(10f); // attendi per 10 secondi
        ScoreController.Instance.scoreAmount = ScoreController.Instance.scoreAmount / 2;
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
