using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coconut_Pickup : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        
            if(other.CompareTag("Player"))
            {
                if(Input.GetKeyDown(KeyCode.F)){
                    Player player = GameObject.Find("Player").GetComponent<Player>();
                    player.inventory.AddItem(new Item { itemType = Item.ItemType.Coconut, amount = 1});
                    this.gameObject.SetActive(false);
                }
            }
        
    }
}
