using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueList dialogueList;

    private DialogueObject currentDialogue;

    private void Start()
    {
        if (dialogueList != null)
        {
            SetRandomDialogue();
        }
    }

    public void UpdateDialogueList(DialogueList dialogueList)
    {
        this.dialogueList = dialogueList;
        SetRandomDialogue();
    }

    private void SetRandomDialogue()
    {
        if (dialogueList != null)
        {
            currentDialogue = dialogueList.GetRandomDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController player))
        {
            player.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
        }
    }

    public void Interact(PlayerController player)
    {
        DialogueResponseEvents matchingResponseEvents = null;

        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObjects.Contains(currentDialogue))
            {
                matchingResponseEvents = responseEvents;
                break;
            }
        }

        if (matchingResponseEvents != null && player != null && player.DialogueUi != null)
        {
            player.DialogueUi.AddResponseEvents(matchingResponseEvents.Events);
        }

        if (player != null && player.DialogueUi != null)
        {
            player.DialogueUi.ShowDialogue(currentDialogue);
        }
    }

}