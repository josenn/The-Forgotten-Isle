using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enable_F_Key : MonoBehaviour
{
    private GameObject sprite;

    private void Awake() {
        sprite = transform.Find("UI key sprite").gameObject;
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            sprite.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            sprite.SetActive(false);
        }
    }
}
