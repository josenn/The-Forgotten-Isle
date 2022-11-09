using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDialActivator : MonoBehaviour
{

    bool dialHasCrystal = false;
    bool delayOver = true;
    Player player;

    private void Start() {
        GameObject playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(delayOver){
            if(other.CompareTag("Player"))
            {
                if(Input.GetKeyDown(KeyCode.F)){

                    // List<Item> _itemList = new List<Item>();
                    // _itemList =  player.inventory.GetItemList();
                    
                     
                    if (dialHasCrystal){
                    
                            StartCoroutine(Activate());   
                    }
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
    
    private IEnumerator Activate()
    {
        Debug.Log("Activating Time Dial....");
        
        yield return null;
    }
}
