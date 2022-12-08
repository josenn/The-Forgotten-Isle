using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_torch : MonoBehaviour
{
    private Transform _respawnPoint;
    private GameObject _blueFire;
    private List<Respawn_torch> _allTorches = new List<Respawn_torch>();
    private Respawn_Handler _respawnHandler;

    public AudioClip torchSFX;
    private AudioSource source;
    
    void Awake()
    {
        _respawnPoint = transform.Find("respawn point");
        _blueFire = transform.Find("blue flame").gameObject;
        _respawnHandler = GameObject.Find("*Respawn Handler").GetComponent<Respawn_Handler>();
        source = GetComponent<AudioSource>();
        foreach (GameObject torch in GameObject.FindGameObjectsWithTag("Respawn Torch"))
        {
            _allTorches.Add(torch.GetComponent<Respawn_torch>());
        }
    }

    public void TurnOffChildren(){
        _blueFire.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            source.clip = torchSFX;
            source.PlayOneShot(source.clip);
            _respawnHandler.currentRespawn = _respawnPoint;
            foreach(Respawn_torch torch in _allTorches){
                torch.TurnOffChildren();
            }
            _blueFire.SetActive(true);
        }
    }
}
