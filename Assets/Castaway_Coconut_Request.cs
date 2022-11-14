using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castaway_Coconut_Request : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject introDialogue;
    [SerializeField] private DialogueObject successDialogue;
    public List<DialogueObject> idleDialogue;
    private Item item = null;
    bool gotTheCoconuts = false;
    int dialogIndex = 0;

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
            if(player.Interactable is Castaway_Coconut_Request ccr && ccr == this)
            {
                player.Interactable = null;
            }
        }
    }
    public void Interact(Player player)
    {
        if (!gotTheCoconuts){
            GetItem(player);
            if (item != null && item.amount >= 3){
                player.DialogueUI.ShowDialogue(successDialogue);
                player.inventory.RemoveItem(new Item { itemType = Item.ItemType.Coconut, amount = 3});
                player.inventory.AddItem(new Item { itemType = Item.ItemType.Crystal, amount = 1});
                gotTheCoconuts = true;

            } else {
            player.DialogueUI.ShowDialogue(introDialogue);

            }
        } else {
            player.DialogueUI.ShowDialogue(idleDialogue[dialogIndex]);
            dialogIndex += 1;
            if (dialogIndex > 5){
                dialogIndex = 0;
            }

        }
    }

    private void GetItem(Player player) {
            foreach (Item _item in player.inventory.itemList){
                if (_item.itemType == Item.ItemType.Coconut){
                    item = _item;
                }
            }
        }
}
