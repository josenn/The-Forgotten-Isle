using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain_Bottle_Request : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject introDialogue;
    [SerializeField] private DialogueObject idleIntroDialogue;
    [SerializeField] private DialogueObject successDialogue;
    [SerializeField] private DialogueObject idleDialogue;
    [SerializeField] private GameObject _transporter;
    private Item item = null;
    bool gotTheBottles = false;
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
            if(player.Interactable is Captain_Bottle_Request thisScript && thisScript == this)
            {
                player.Interactable = null;
            }
        }
    }
    public void Interact(Player player)
    {
        if (!gotTheBottles){
            GetItem(player);
            if (item != null && item.amount >= 5){
                player.DialogueUI.ShowDialogue(successDialogue);
                player.inventory.RemoveItem(new Item { itemType = Item.ItemType.Bottle, amount = 5});
                gotTheBottles = true;
                _transporter.SetActive(true);

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
                if (_item.itemType == Item.ItemType.Bottle){
                    item = _item;
                }
            }
        }
}
