using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDialActivator : MonoBehaviour
{

    bool dialHasCrystal = false;
    Inventory inventory;
    bool delayOver = true;
    Player player;

    private void Start() {
        //GameObject inventoryManager = GameObject.Find("Inventory Manager");
        //inventory = inventoryManager.GetComponent<Inventory>();
        //GameObject playerObject = GameObject.Find("Player");
        //player = playerObject.GetComponent<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(delayOver){
            if(other.CompareTag("Player"))
            {
                if(Input.GetKeyDown(KeyCode.F)){
                    if (dialHasCrystal){
                    
                            StartCoroutine(Activate());   
                    }
                    if (!dialHasCrystal){
                        //if(inventory.Crystal == true)
                        //{
                            delayOver = false;
                            StartCoroutine(Delay());
                            Transform crystal = this.transform.Find("Crystal");
                            crystal.gameObject.SetActive(true);
                            dialHasCrystal = true;
                        //}
                    }
                }
            }
        }
    }

    private IEnumerator Delay(){
        yield return new WaitForSeconds(0.5f);
        delayOver = true;
    }
    
    private IEnumerator Activate()
    {
        Debug.Log("Activating Time Dial....");
        
        yield return null;
    }
}
