using UnityEngine;

public class Get_Wood_From_Boat : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject gettingWoodDialogue;
    [SerializeField] private DialogueObject noMoreWoodDialogue;
    bool gotWood = false;

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
            if(player.Interactable is Get_Wood_From_Boat thisScript && thisScript == this)
            {
                player.Interactable = null;
            }
        }
    }
    public void Interact(Player player)
    {
        if (!gotWood){
            player.DialogueUI.ShowDialogue(gettingWoodDialogue);
            player.inventory.AddItem(new Item { itemType = Item.ItemType.Wood, amount = 1});
            gotWood = true;
        }
        else{
            player.DialogueUI.ShowDialogue(noMoreWoodDialogue);
        }
    }
}