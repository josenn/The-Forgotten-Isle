using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType {
        Crystal,
        Ticket,
        Coconut,
        Bottle,
        Wood,
        Fish,
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
            case ItemType.Bottle: return ItemAssets.Instance.bottleSprite;
            case ItemType.Wood: return ItemAssets.Instance.woodSprite;
            case ItemType.Fish: return ItemAssets.Instance.fishSprite;
            //create new case here for sprite retrieval, also add public sprite to ItemAssets
            
        }
    }
    public bool IsStackable() {
        switch (itemType) {
            default:
            case ItemType.Crystal:
            case ItemType.Ticket:
            case ItemType.Coconut:
            case ItemType.Bottle:
            case ItemType.Wood:
            case ItemType.Fish:
                return true;
            // group all nonstackables and stackables together, with 'return false' and 'return true' under each group
        }
    }
}
