using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Fisher : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject introDialogue;
    [SerializeField] private DialogueObject idleIntroDialogue;
    [SerializeField] private DialogueObject successDialogue;
    [SerializeField] private DialogueObject idleDialogue;
    private Item item = null;
    bool gotTheFish = false;
    bool seenIntroDia = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            player.Interactable = this;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            if(player.Interactable is Ice_Fisher thisScript && thisScript == this)
            {
                player.Interactable = null;
            }
        }
    }
    public void Interact(Player player)
    {
        if (!gotTheFish){
            GetItem(player);
            if (item != null && item.amount >= 3){
                player.DialogueUI.ShowDialogue(successDialogue);
                player.inventory.RemoveItem(new Item { itemType = Item.ItemType.Fish, amount = 3});
                player.inventory.AddItem(new Item { itemType = Item.ItemType.Wood, amount = 1});
                gotTheFish = true;

            } else if (!seenIntroDia){
            player.DialogueUI.ShowDialogue(introDialogue);
            seenIntroDia = true;
            } else{
            player.DialogueUI.ShowDialogue(idleIntroDialogue);
            }

            
        } else {
            player.DialogueUI.ShowDialogue(idleDialogue);
            

        }
    }

    private void GetItem(Player player) {
            foreach (Item _item in player.inventory.itemList){
                if (_item.itemType == Item.ItemType.Fish){
                    item = _item;
                }
            }
        }
}
