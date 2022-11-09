using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Pickup : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        
            if(other.CompareTag("Player"))
            {
                if(Input.GetKeyDown(KeyCode.F)){
                    Player player = GameObject.Find("Player").GetComponent<Player>();
                    player.inventory.AddItem(new Item { itemType = Item.ItemType.Crystal, amount = 1});
                    this.gameObject.SetActive(false);
                }
            }
        
    }
}
