using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType {
        Crystal,
        Ticket,
        Coconut,
        // add new item names here
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite() {
        switch (itemType) {
            default:
            case ItemType.Crystal: return ItemAssets.Instance.crystalSprite;
            case ItemType.Ticket: return ItemAssets.Instance.ticketSprite;
            case ItemType.Coconut: return ItemAssets.Instance.coconutSprite;
            //create new case here for sprite retrieval, also add public sprite to ItemAssets
            
        }
    }
    public bool IsStackable() {
        switch (itemType) {
            default:
            case ItemType.Crystal:
            case ItemType.Ticket:
            case ItemType.Coconut:
                return true;
            // group all nonstackables and stackables together, with 'return false' and 'return true' under each group
        }
    }
}
