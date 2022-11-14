using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDialActivator : MonoBehaviour
{

    public bool dialHasCrystal = false;
    public bool delayOver = true;
    Player player;
    public Transform teleportDestination;
    public TimeDialActivator sisterTimeDial;
    public bool sisterHasCrystal = false;
    Transform crystal;
    Animator crystalAnim;
    Animator sisterCrystalAnim;
    

    private void Start() {
        GameObject playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
        crystal = this.transform.Find("Crystal");
        crystalAnim = crystal.transform.Find("Point Light").gameObject.GetComponent<Animator>();
        Transform sisterCrystal = sisterTimeDial.gameObject.transform.Find("Crystal");
        sisterCrystalAnim = sisterCrystal.transform.Find("Point Light").gameObject.GetComponent<Animator>();
    }

    private void Update(){
        sisterHasCrystal = sisterTimeDial.dialHasCrystal;
        teleportDestination = sisterTimeDial.transform.Find("Teleport point");
    }

    private void OnTriggerStay(Collider other)
    {
        if(delayOver){
            if(other.CompareTag("Player"))
            {
                if(Input.GetKeyDown(KeyCode.F)){

                    
                    if (!dialHasCrystal){
                        int index = player.inventory.itemList.FindIndex(Item => Item.itemType == Item.ItemType.Crystal);
                        if(index >= 0)
                        {
                            player.inventory.RemoveItem(new Item { itemType = Item.ItemType.Crystal, amount = 1});
                            delayOver = false;
                            StartCoroutine(Delay(1f));
                            crystal.gameObject.SetActive(true);
                            dialHasCrystal = true;
                        }
                    } else {
                        // delayOver=false;
                        // StartCoroutine(Delay(6f));
                        // crystalAnim.SetTrigger("GlowUp");
                        // sisterCrystalAnim.SetTrigger("GlowUp");

                    }
                }
            }
        }
    }

    private IEnumerator Delay(float seconds){
        yield return new WaitForSeconds(seconds);
        delayOver = true;
    }

}   
