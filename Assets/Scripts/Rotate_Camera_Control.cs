using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Camera_Control : MonoBehaviour
{
    bool isTurning = false;
    public float lerpTime = 1.5f;
    float velocity;
    public float turnOnE = -90f;
    public float turnOnQ = 90f;

    public AudioClip turnSFX;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTurning){
            if (Input.GetKeyDown(KeyCode.Q)){
                StartCoroutine(RotateCamera(turnOnQ));
                source.clip = turnSFX;
                source.PlayOneShot(source.clip);
            }
            if (Input.GetKeyDown(KeyCode.E)){
                StartCoroutine(RotateCamera(turnOnE));
                source.clip = turnSFX;
                source.PlayOneShot(source.clip);
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
