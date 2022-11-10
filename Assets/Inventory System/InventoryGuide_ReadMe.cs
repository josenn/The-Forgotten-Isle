using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGuide_ReadMe : MonoBehaviour
{
    // How to add Inventory Items!
    // 1. Go to the script "Item" and add the new item's name under 'public enum ItemType' with a comma after.
    //      - Add a case for sprite retrieval (we'll create what this is referencing in a few steps)
    //      - Add a case for stackable or unstackable. 
    // 2. Go to the script ItemAssets and add a public sprite for the new item, make sure it's the same name you referenced before. 
    //      - assign a sprite for this in the editor, it should be 16x16 single sprite at 16 pixels per unit
    // 3. Create your new prefab, and attach a script for adding items to the inventory onto it. 
    //      You can model this script off of Crystal_pickup or Ticket_Pickup. The important thing to know is how to add or remove items:
    //      Player contains a reference to Inventory, which contains a list of items that gets updated. So anytime you want to add or
    //      remove items, you'll need a reference to Player, like this:   Player player = GameObject.Find("Player").GetComponent<Player>();   
    //      To add an item then, write this:   player.inventory.AddItem(new Item { itemType = Item.ItemType.Ticket, amount = 1});
    //      When that gets executed, it will automatically update the inventory.
    //      Similarly, when you want to remove an item, write it like this: player.inventory.RemoveItem(new Item { itemType = Item.ItemType.Crystal, amount = 1});
    //      With both of these of course, the ItemType and amount you'll change to what you need. 

}
