using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticket_Pickup : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        
            if(other.CompareTag("Player"))
            {
                if(Input.GetKeyDown(KeyCode.F)){
                    Player player = GameObject.Find("Player").GetComponent<Player>();
                    player.inventory.AddItem(new Item { itemType = Item.ItemType.Ticket, amount = 1});
                    this.gameObject.SetActive(false);
                }
            }
        
    }
}
