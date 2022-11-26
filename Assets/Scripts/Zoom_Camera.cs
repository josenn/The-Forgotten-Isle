using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom_Camera : MonoBehaviour
{
    public Camera camera;
    bool isZooming = false;
    public float lerpTime = 1.5f;
    public float closeSize;
    public float mediumSize;
    public float farSize;
    int currentSize = 1;
    float targetSize;

    void Update()
    {
        if (!isZooming){
            if (Input.GetKeyDown(KeyCode.R)){
                StartCoroutine(ZoomCamera());
            }
        }

    }

    private IEnumerator ZoomCamera (){

        isZooming = true;
        
        float currentCameraSize = camera.orthographicSize;
        
        if (currentSize == 0){
            targetSize = mediumSize;
        }
        if (currentSize == 1){
            targetSize = farSize;
        }
        if (currentSize == 2){
            targetSize = closeSize;
        }

        float elapsedTime = 0f;    

        while (elapsedTime < lerpTime){
            float t = elapsedTime/lerpTime;
            t = t * t * (3f - 2f * t);
            camera.orthographicSize = Mathf.Lerp(currentCameraSize, targetSize, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        camera.orthographicSize = targetSize;
        
        currentSize++;       
        if (currentSize == 3){
            currentSize = 0;
        }

        isZooming = false;
        yield return null;
    }
}
