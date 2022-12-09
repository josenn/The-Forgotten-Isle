using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Pickup : MonoBehaviour
{
   public AudioClip pickupSFX;

    private void OnTriggerEnter(Collider other)
    {
        
            if(other.CompareTag("Player"))
            {
                
                    Player player = GameObject.Find("@Player").GetComponent<Player>();
                    player.inventory.AddItem(new Item { itemType = Item.ItemType.Fish, amount = 1});
                    player.PlayPickup(pickupSFX);
                    this.gameObject.SetActive(false);
                
            }
        
    }
}
