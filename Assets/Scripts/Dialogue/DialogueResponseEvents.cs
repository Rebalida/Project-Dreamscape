using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class DialogueResponseEvents : MonoBehaviour
{
    [SerializeField] private List<DialogueObject> dialogueObjects = new List<DialogueObject>();
    [SerializeField] private ResponseEvent[] events;

    public List<DialogueObject> DialogueObjects => dialogueObjects;
    public ResponseEvent[] Events => events;

    public void OnValidate()
    {
        if (dialogueObjects == null || dialogueObjects.Count == 0) return;

        List<DialogueObject> uniqueDialogues = new List<DialogueObject>(new HashSet<DialogueObject>(dialogueObjects));

        int totalResponses = dialogueObjects.Sum(dialogueObject => dialogueObject.Responses.Length);

        if (events != null && events.Length == totalResponses) return;

        if (events == null)
        {
            events = new ResponseEvent[totalResponses];
        }
        else
        {
            Array.Resize(ref events, totalResponses);
        }

        int index = 0;
        foreach (DialogueObject dialogueObject in dialogueObjects)
        {
            foreach (Response response in dialogueObject.Responses)
            {
                if (index < totalResponses)
                {
                    if (events[index] != null)
                    {
                        events[index].name = response.ResponseText;
                    }
                    else
                    {
                        events[index] = new ResponseEvent() { name = response.ResponseText };
                    }
                    index++;
                }
            }
        }
    }
}
