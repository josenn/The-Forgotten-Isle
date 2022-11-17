using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_Trigger : MonoBehaviour
{
    private Respawn_Handler _respawnHandler;
    
    private void Awake() {
        _respawnHandler = GameObject.Find("Respawn Handler").GetComponent<Respawn_Handler>();

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("RespawnOnContact")){
            
            transform.position = _respawnHandler.currentRespawn.position;
            Debug.Log("player collided with water");
        }
    }
}
