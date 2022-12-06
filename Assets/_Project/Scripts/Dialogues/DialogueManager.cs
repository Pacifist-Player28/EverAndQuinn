using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogues")]
    public TMP_Text dialogueText;
    public GameObject dialogueUi;
    public UnityEvent enableCollider;

    private Queue<string> sentences;
    private PlayerMovementKeyboard playerMovement;

    private void Start()
    {
        sentences = new Queue<string>();
        playerMovement = FindObjectOfType<PlayerMovementKeyboard>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        //enable UI
        dialogueUi.SetActive(true);
        //disable Player movement
        playerMovement.enabled = false;

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
        dialogueText.text = sentence.ToString();
    }

    public void EndDialogue()
    {
        //disable UI
        dialogueUi.SetActive(false);

        enableCollider.Invoke();

        //enable Player movement
        playerMovement.enabled = true;
    }
}
