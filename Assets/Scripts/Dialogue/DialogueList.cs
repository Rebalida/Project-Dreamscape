using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueList")]
public class DialogueList : ScriptableObject
{
    [SerializeField] private DialogueObject[] dialogues;

    public DialogueObject GetRandomDialogue()
    {
        if (dialogues.Length == 0)
        {
            Debug.LogError("DialogueList is empty!");
            return null;
        }

        int randomIndex = Random.Range(0, dialogues.Length);
        return dialogues[randomIndex];
    }
}