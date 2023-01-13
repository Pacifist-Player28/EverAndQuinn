using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;
using Inventory;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogues")]
    public TMP_Text dialogueText;
    public GameObject dialogueUi;
    public UnityEvent enableCollider;
    public GameObject spriteLeft, spriteRight;
    [Space]
    public float animationSpeed;

    private Queue<string> sentences;
    private PlayerMovementKeyboard playerMovement;
    private int sentenceCount;


    private void Start()
    {
        sentences = new Queue<string>();
        playerMovement = FindObjectOfType<PlayerMovementKeyboard>();
    }

    private void Update()
    {
        Debug.Log("Rounds: " + sentenceCount);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        //enable UI
        dialogueUi.SetActive(true);
        //disable Player movement
        playerMovement.enabled = false;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        sentenceCount = sentenceCount + 2;
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence.ToString();
        DisplayNextSprite(FindObjectOfType<DialogueTrigger>().dialogue);
    }

    public void DisplayNextSprite(Dialogue dialogue)
    {

        if (sentences.Count == 0)
        {
            return;
        }

        if (sentenceCount < dialogue.sprites.Length)
        {
            //spriteRight.GetComponent<Image>().sprite = dialogue.sprites[sentenceCount + 1];
            StartCoroutine(SwitchSprites(dialogue));
        }
    }

    public void EndDialogue()
    {
        //disable UI
        dialogueUi.SetActive(false);

        enableCollider.Invoke();

        //enable Player movement
        playerMovement.enabled = true;

        sentenceCount = 0;
    }

    public void AddItemToInventory(ItemSetting item)
    {
        item.collected = true;
    }

    IEnumerator SwitchSprites(Dialogue dialogue)
    {
        int index = sentenceCount;
        while (sentenceCount == index)
        {
            spriteRight.GetComponent<Image>().sprite = dialogue.sprites[sentenceCount - 1];
            yield return new WaitForSeconds(animationSpeed);
            spriteRight.GetComponent<Image>().sprite = dialogue.sprites[sentenceCount - 2];
            yield return new WaitForSeconds(animationSpeed);
        }
    }
}