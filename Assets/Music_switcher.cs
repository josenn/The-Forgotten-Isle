using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_switcher : MonoBehaviour
{
    AudioSource audio;
    public float fadeIncrement;
    public float fadeTime;

    void Start()
    {
        audio = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            StartCoroutine(FadeMusic(1f));
            Debug.Log("entered " + gameObject.name.ToString());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            StartCoroutine(FadeMusic(0f));
            Debug.Log("exited " + gameObject.name.ToString());
        }
    }

    private IEnumerator FadeMusic(float target){
        if (target == 1f) {
            while (audio.volume < target){
                audio.volume += fadeIncrement;
                yield return new WaitForSeconds(fadeTime); 
            }
        }
        if (target == 0f){
            while (audio.volume > target){
                audio.volume -= fadeIncrement;
                yield return new WaitForSeconds(fadeTime); 
            }
        }
    }
}
