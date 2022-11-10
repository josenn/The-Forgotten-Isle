using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    private TypewriterEffect typewriterEffect;
    private ResponseHandler responseHandler;
    public bool IsOpen { get; private set; }
    public Player player;

    void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        player.allowedToMove = false;
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for(int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return typewriterEffect.Run(dialogue, textLabel);

            if(i == dialogueObject.Dialogue.Length -1 && dialogueObject.HasResponses) break;

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        }

        if(dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }

    }

    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        IsOpen = false;
        textLabel.text = string.Empty;
        player.allowedToMove = true;
    }
}
