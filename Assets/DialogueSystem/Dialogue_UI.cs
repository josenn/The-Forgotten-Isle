using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.InputSystem;

public class Dialogue_UI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogObject testDialogue;
    [SerializeField] private GameObject dialogueBox;
    public GameObject textFinishedIndicator;

    public DialogueBrain dialogueBrain;

    //public PlayerInput playerInput;
   
    
    private Typewriter_effect typeWriterEffect;
    private ResponseHandler responseHandler;

    private void Start() {
        typeWriterEffect = GetComponent<Typewriter_effect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        //ShowDialogue(testDialogue);
        
    }

    

    public void ShowDialogue(DialogObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
        
        //playerInput.actions.Disable();
        textFinishedIndicator.SetActive(false);
        
    }

    public void AddResponseEvents(Response_Event[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogue(DialogObject dialogueObject)
    {
        

        for (int i = 0; i <dialogueObject.Dialogue.Length; i++)
        {
            textFinishedIndicator.SetActive(false);

            string dialogue = dialogueObject.Dialogue[i];
            yield return typeWriterEffect.Run(dialogue, textLabel);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            textFinishedIndicator.SetActive(true);
            
            yield return new WaitForSeconds(0.3f);

            yield return new WaitUntil (() => Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogueObject.HasResponses)
        {
            StartCoroutine(responseHandler.ShowResponses(dialogueObject.Responses));
        }
        else
        {
            CloseDialogueBox();
        }
    }

    public void CloseDialogueBox()
    {   
        
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        //playerInput.actions.Enable();
        dialogueBrain.SetNPCDialogue();
        
    }
}
