using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDialActivator : MonoBehaviour, IInteractable
{

    public bool dialHasCrystal = false;
    public bool delayOver = true;
    Player player;
    public Transform teleportDestination;
    public TimeDialActivator sisterTimeDial;
    public bool sisterHasCrystal = false;
    Transform crystal;
    Animator crystalAnim;
    Animator sisterCrystalAnim;
    [SerializeField] DialogueObject _noCrystalInInventory;
    [SerializeField] DialogueObject _otherDialNoCrystal;
    private Item item = null;

    public AudioClip activateSFX;
    private AudioSource source;

    private void Start() {
        GameObject playerObject = GameObject.Find("Player");
        source = GetComponent<AudioSource>();
        player = playerObject.GetComponent<Player>();
        crystal = this.transform.Find("Crystal");
        crystalAnim = crystal.transform.Find("Point Light").gameObject.GetComponent<Animator>();
        Transform sisterCrystal = sisterTimeDial.gameObject.transform.Find("Crystal");
        sisterCrystalAnim = sisterCrystal.transform.Find("Point Light").gameObject.GetComponent<Animator>();
    }

    private void Update(){
        sisterHasCrystal = sisterTimeDial.dialHasCrystal;
        teleportDestination = sisterTimeDial.transform.Find("Teleport point");
    }

    private void OnTriggerStay(Collider other)
    {
        if(delayOver){
            if(other.CompareTag("Player"))
            {
                if(Input.GetKeyDown(KeyCode.F)){

                    
                    if (!dialHasCrystal){
                        int index = player.inventory.itemList.FindIndex(Item => Item.itemType == Item.ItemType.Crystal);
                        if(index >= 0)
                        {
                            player.inventory.RemoveItem(new Item { itemType = Item.ItemType.Crystal, amount = 1});
                            delayOver = false;
                            StartCoroutine(Delay(1f));
                            source.clip = activateSFX;
                            source.PlayOneShot(source.clip);
                            crystal.gameObject.SetActive(true);
                            dialHasCrystal = true;
                        }
                    } else {
                        // delayOver=false;
                        // StartCoroutine(Delay(6f));
                        // crystalAnim.SetTrigger("GlowUp");
                        // sisterCrystalAnim.SetTrigger("GlowUp");

                    }
                }
            }
        }
    }

    private IEnumerator Delay(float seconds){
        yield return new WaitForSeconds(seconds);
        delayOver = true;
    }

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
            if(player.Interactable is TimeDialActivator ccr && ccr == this)
            {
                player.Interactable = null;
            }
        }
    }
     public void Interact(Player player){
        GetItem(player);
        if (!dialHasCrystal) {
            if (item == null){
                player.DialogueUI.ShowDialogue(_noCrystalInInventory);
            }
        }
        if (dialHasCrystal && !sisterHasCrystal) {
            player.DialogueUI.ShowDialogue(_otherDialNoCrystal);
        }
        

    }

     
     private void GetItem(Player player) {
            foreach (Item _item in player.inventory.itemList){
                if (_item.itemType == Item.ItemType.Crystal){
                    item = _item;
                }
            }
        }
    
}   
