using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC_DialogueTrigger : MonoBehaviour
{
    public DialogObject assignedDialogue;
    public Dialogue_UI dialogueUI;
    public string characterNameToDisplay;
    public TextMeshProUGUI nameTextBox;
    public Color nameColor;

    public void UpdateDialogueObject(DialogObject dialogueObject)
    {
        this.assignedDialogue = dialogueObject;
    }

    
    public void PlayMyAssignedDialogue()
    {
        if (TryGetComponent(out DialogueResponseEvents responseEvents) && responseEvents.DialogueObject == assignedDialogue)
        {
            dialogueUI.AddResponseEvents(responseEvents.Events);
        }
        dialogueUI.ShowDialogue(assignedDialogue);
        if(nameTextBox != null)
        {    
        nameTextBox.text = characterNameToDisplay;
        nameTextBox.color = nameColor;
        }
    }

}
