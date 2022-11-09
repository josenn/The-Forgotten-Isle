using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType {
        Crystal,
        Ticket,
        // add new item names here
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite() {
        switch (itemType) {
            default:
            case ItemType.Crystal: return ItemAssets.Instance.crystalSprite;
            case ItemType.Ticket: return ItemAssets.Instance.ticketSprite;
            //create new case here for sprite retrieval, also add public sprite to ItemAssets
            
        }
    }
    public bool IsStackable() {
        switch (itemType) {
            default:
            case ItemType.Crystal:
                return false;
            case ItemType.Ticket:
                return true;
            // group all nonstackables and stackables together, with 'return false' and 'return true' under each group
        }
    }
}
