using System;
using UnityEngine;

public class DialogueResponseEvents : MonoBehaviour
{
    [SerializeField] private DialogObject dialogueObject;
    [SerializeField] private Response_Event[] events;

    public Response_Event[] Events => events;
    public DialogObject DialogueObject => dialogueObject;

    public void OnValidate()
    {
        if (dialogueObject == null) return;
        if (dialogueObject.Responses == null) return;
        if (events != null && events.Length == dialogueObject.Responses.Length) return;

        

        if (events == null)
        {
            events = new Response_Event[dialogueObject.Responses.Length];
        }
        else
        {
            Array.Resize(ref events, dialogueObject.Responses.Length);
        }

        for (int i = 0; i < dialogueObject.Responses.Length; i++)
        {
            Responses response = dialogueObject.Responses[i];

            if (events[i] != null)
            {
                events[i].name = response.ResponseTextPos;
                
                continue;
            }

            events[i] = new Response_Event() {name = response.ResponseTextPos};
            
        }
    }
}
