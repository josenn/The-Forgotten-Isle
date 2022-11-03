using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Camera_Control : MonoBehaviour
{
    bool isTurning = false;
    public float lerpTime = 1.5f;
    float velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTurning){
            if (Input.GetKeyDown(KeyCode.Q)){
                StartCoroutine(RotateCamera(90f));
            }
            if (Input.GetKeyDown(KeyCode.E)){
                StartCoroutine(RotateCamera(-90f));
            }
        }


    }

    private IEnumerator RotateCamera (float whichWay){

        isTurning = true;
        
        Quaternion currentRotation = transform.rotation;
        
        Quaternion addToTurn = Quaternion.Euler(0, whichWay, 0);

        Quaternion howToTurn = currentRotation * addToTurn;

        float elapsedTime = 0f;    

        while (elapsedTime < lerpTime){
            float t = elapsedTime/lerpTime;
            t = t * t * (3f - 2f * t);
            transform.rotation = Quaternion.Lerp(currentRotation, howToTurn, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = howToTurn;

        isTurning = false;
        yield return null;
    }
}
