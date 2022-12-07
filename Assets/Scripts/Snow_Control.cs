using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow_Control : MonoBehaviour
{
    private GameObject _snow;

    
    void Start()
    {
        _snow = GameObject.Find("Snow Particle sys");
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("TropicalZone")){
            _snow.SetActive(false);
        }
        if (other.gameObject.CompareTag("FrozenZone")){
            _snow.SetActive(true);
        }
    }
    
}
