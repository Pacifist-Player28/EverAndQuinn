using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public TMP_Text dialogueText;
    public GameObject dialogueUi;
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
        Debug.Log(sentence);
        dialogueText.text = sentence.ToString();
    }

    void EndDialogue()
    {
        dialogueUi.SetActive(false);
        playerMovement.enabled = true;
        Debug.Log("--End of conversation--");
    }
}
