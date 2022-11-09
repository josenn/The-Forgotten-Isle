using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType {
        Crystal,
        Ticket,
    
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite() {
        switch (itemType) {
            default:
            case ItemType.Crystal: return ItemAssets.Instance.crystalSprite;
            case ItemType.Ticket: return ItemAssets.Instance.ticketSprite;
            
        }
    }
}
