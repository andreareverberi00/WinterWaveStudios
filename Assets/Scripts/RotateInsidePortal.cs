using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInsidePortal : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(50, 0, 0); // Velocità di rotazione dell'oggetto lungo gli assi x, y e z

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
