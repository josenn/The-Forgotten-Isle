using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    bool isMoving = false;
    bool atFirstLocation = true;

    public float lerpTime = 4.5f;

    public Transform objectToMove;

    public Transform locationA;
    public Transform locationB;
    Vector3 whereToMoveTo;

    public AudioClip buttonSFX;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other) {
        if (!isMoving) {   
             if (other.gameObject.CompareTag("Player")){
                if (Input.GetKeyDown(KeyCode.F)){
                    StartCoroutine(MoveIt());
                }
            }
        }
    }

    private IEnumerator MoveIt(){
        isMoving = true;
        float elapsedTime = 0f;  
        Vector3 pressDown = new Vector3(0f, 0.5f, 0f);
        source.clip = buttonSFX;
        source.PlayOneShot(source.clip);
        if (atFirstLocation){
            whereToMoveTo = locationB.position;
            transform.position -= pressDown;
        }else{
            whereToMoveTo = locationA.position;
            transform.position += pressDown;
        }
        
        atFirstLocation = !atFirstLocation;

        while (elapsedTime < lerpTime){
            float t = elapsedTime/lerpTime;
            t = t * t * (3f - 2f * t);
            objectToMove.position = Vector3.Lerp(objectToMove.position, whereToMoveTo, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        objectToMove.position = whereToMoveTo;

        isMoving = false;
        yield return null;
    }
}
