using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResponseHandler : MonoBehaviour
{
    private Dialogue_UI dialogueUI;
    private Move_Cursor moveCursor;
    [SerializeField] private TextMeshProUGUI positiveResponse;
    [SerializeField] private TextMeshProUGUI negativeResponse;
    [SerializeField] private GameObject responseParent;

    private Response_Event[] responseEvents;

    private void Start() {
        moveCursor = GetComponent<Move_Cursor>();
        dialogueUI = GetComponent<Dialogue_UI>();
    }

    public void AddResponseEvents(Response_Event[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }

    public IEnumerator ShowResponses(Responses[] responses)
    {
        

        for (int i = 0; i <responses.Length; i++)
        {
            Responses response = responses[i];
            int responseIndex = i;

            responseParent.SetActive(true);
            positiveResponse.text = response.ResponseTextPos;
            negativeResponse.text = response.ResponseTextNeg;
            //yield return new WaitForSeconds(0.3f);
            yield return new WaitUntil (() => Input.GetKeyDown(KeyCode.Space));
            if (moveCursor.responsePosOrNeg)
                OnPickedResponse(responses[0], responseIndex);
            else if (moveCursor.responsePosOrNeg == false)
                OnPickedResponse(responses[1], responseIndex);
        }
    }

    

    private void OnPickedResponse(Responses response, int responseIndex)
    {
        responseParent.SetActive(false);

        if (moveCursor.responsePosOrNeg)
        {
            if (responseEvents != null && responseIndex <= responseEvents.Length)
            {
                responseEvents[0].OnPickedResponse?.Invoke();
            }
        }
        else if (moveCursor.responsePosOrNeg == false)
        {
            if (responseEvents != null && responseIndex <= responseEvents.Length)
            {
                responseEvents[1].OnPickedResponse?.Invoke();
            }
        }

        responseEvents = null;

        if(response.DialogObject)
        {
            dialogueUI.ShowDialogue(response.DialogObject);
        }
        else
        {
            dialogueUI.CloseDialogueBox();
        }

    }
}
