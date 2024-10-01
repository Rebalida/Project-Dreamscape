using System.Collections;
using UnityEngine;
using TMPro;
public class DialogueUi : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;

    public bool IsOpen { get; private set; }

    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;

    public static DialogueUi instance;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();

        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject) 
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogueRoutine(dialogueObject));
        Debug.Log("Dialogue Open");
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents) 
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogueRoutine(DialogueObject dialogueObject) 
    {

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++) 
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffectRoutine(dialogue);

            textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasReponses) break;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        }

        if (dialogueObject.HasReponses) 
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
    }

    private IEnumerator RunTypingEffectRoutine(string dialogue) 
    {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.F))
            {
                typewriterEffect.Stop();
            }
        }
    }

    public void CloseDialogueBox() 
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
