using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public GameObject dialogueUi;

    private Queue<string> sentences;
    private PlayerMovementKeyboard playerMovement;

    private void Start()
    {
        sentences = new Queue<string>();
        playerMovement = FindObjectOfType<PlayerMovementKeyboard>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueUi.SetActive(true);
        playerMovement.enabled = false;
        Debug.Log("starting conversation with " + dialogue.name);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) sentences.Enqueue(sentence);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //Debug.Log(sentence);
        dialogueText.text = sentence.ToString();
    }

    public void EndDialogue()
    {
        dialogueUi.SetActive(false);

        playerMovement.enabled = true;
        //send an Event to DialogueTrigger!
    }
}
