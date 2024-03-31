using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoSingleton<CameraView>
{
    // Start is called before the first frame update
    [HideInInspector] public float mincamera;
    [HideInInspector] public float maxcamera;

        void Start()
        {
            Camera camera = Camera.main;

            float cameraWidth = 2f * camera.orthographicSize * camera.aspect;
            float cameraHeight = 2f * camera.orthographicSize;

            float cameraMinX = camera.transform.position.x - cameraWidth / 2f;
            float cameraMaxX = camera.transform.position.x + cameraWidth / 2f;

            float cameraMinY = camera.transform.position.y - cameraHeight / 2f;
            float cameraMaxY = camera.transform.position.y + cameraHeight / 2f;
        maxcamera = cameraMaxX;
        mincamera = cameraMinX;
            /*Debug.Log("Camera Bounds:");
            Debug.Log("Min X: " + cameraMinX);
            Debug.Log("Max X: " + cameraMaxX);
            Debug.Log("Min Y: " + cameraMinY);
            Debug.Log("Max Y: " + cameraMaxY);*/
        }
    


}
