using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Materials : MonoBehaviour
{
    public MeshRenderer[] meshesToChange;
    public Material[] tropicalMaterials;
    public Material[] frozenMaterials;
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("FrozenZone")) {
            for(int i = 0; i < meshesToChange.Length; i++) {
                meshesToChange[i].material = frozenMaterials[i];
            }
        }   

        if (other.gameObject.CompareTag("TropicalZone")) {
            for(int i = 0; i < meshesToChange.Length; i++) {
                meshesToChange[i].material = tropicalMaterials[i];
            }
        } 
    }
}
