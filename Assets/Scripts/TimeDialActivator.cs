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

    private void Start() {
        GameObject playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
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
                if(Input.GetKeyUp(KeyCode.F)){

                    
                    if (!dialHasCrystal){
                        int index = player.inventory.itemList.FindIndex(Item => Item.itemType == Item.ItemType.Crystal);
                        if(index >= 0)
                        {
                            player.inventory.RemoveItem(new Item { itemType = Item.ItemType.Crystal, amount = 1});
                            delayOver = false;
                            StartCoroutine(Delay());
                            Transform crystal = this.transform.Find("Crystal");
                            crystal.gameObject.SetActive(true);
                            dialHasCrystal = true;
                        }
                    }
                }
            }
        }
    }

    private IEnumerator Delay(){
        yield return new WaitForSeconds(1f);
        delayOver = true;
    }

}   
