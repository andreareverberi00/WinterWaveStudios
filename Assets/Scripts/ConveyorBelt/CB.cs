using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CB : MonoBehaviour
{
    [SerializeField]
    private float objectSpeed = 5f; // Velocità degli oggetti sul nastro
    [SerializeField]
    private Vector3 direction = Vector3.forward; // Direzione del movimento degli oggetti
    [SerializeField]
    private List<GameObject> onBelt = new List<GameObject>(); // Lista degli oggetti sul nastro
    [SerializeField]
    private Material material; // Materiale con la texture del nastro
    [SerializeField]
    private float textureSpeedMultiplier = 1f; // Moltiplicatore per calibrare la velocità della texture

    private float textureOffset = 0f; // Offset corrente della texture

    void Update()
    {
        // Aggiorna la posizione della texture basata sulla velocità degli oggetti
        textureOffset += objectSpeed * textureSpeedMultiplier * Time.deltaTime;
        material.mainTextureOffset = new Vector2(0, textureOffset);

        // Aggiorna la posizione di ogni oggetto sul nastro
        foreach (var item in onBelt)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            rb.velocity = direction * objectSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("YourObjectTag")) // Sostituisci con il tag appropriato
        {
            onBelt.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}
