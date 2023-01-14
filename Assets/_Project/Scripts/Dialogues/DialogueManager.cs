using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using Inventory;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogues")]
    public TMP_Text dialogueText;
    public GameObject dialogueUi;
    public UnityEvent enableCollider;
    public GameObject spriteLeft, spriteRight;
    [Space]
    public float animationSpeed;
    public float textDelay = 0.01f;
    public GameObject[] interactables;
    public Vector2[] distanceToInteractables;

    private Queue<string> sentences;
    private PlayerMovementKeyboard playerMovement;
    private DialogueTrigger activeDialogueTrigger;
    private int spriteCount = 0;

    private void Start()
    {
        spriteCount = 0;
        sentences = new Queue<string>();
        playerMovement = FindObjectOfType<PlayerMovementKeyboard>();
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
    }

    private void Update()
    {
        MeasureAndActivate();
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
        spriteCount += 2;
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //string sentence = sentences.Dequeue();
        //dialogueText.text = sentences.Dequeue().ToString();
        StartCoroutine(TextAnimation(sentences.Dequeue().ToString()));
        DisplayNextSprites(activeDialogueTrigger.dialogue);

        Debug.Log("Script: " + FindObjectOfType<DialogueTrigger>().name);
    }

    public void DisplayNextSprites(Dialogue dialogue)
    {
        if (spriteCount < dialogue.spritesRight.Length)
        {
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

        spriteCount -= spriteCount;
    }

    public void AddItemToInventory(ItemSetting item)
    {
        item.collected = true;
    }

    public void MeasureAndActivate()
    {
        //this method activates the nearest interactable and disables every other inside the scene.
        distanceToInteractables = new Vector2[interactables.Length];
        Vector2 smallestVector = new Vector2(float.MaxValue, float.MaxValue);
        GameObject nearestInteractable = null;
        GameObject[] otherInteractables = new GameObject[interactables.Length - 1];
        int nearestIndex = -1;
        int index = 0;

        for (int i = 0; i < interactables.Length; i++)
        {
            distanceToInteractables[i] = interactables[i].transform.position - playerMovement.transform.position;
        }

        for (int i = 0; i < distanceToInteractables.Length; i++)
        {
            if (distanceToInteractables[i].magnitude < smallestVector.magnitude)
            {
                smallestVector = distanceToInteractables[i];
                nearestIndex = i;
            }
        }

        if(nearestIndex != -1)
        {
            nearestInteractable = interactables[nearestIndex];
            Debug.Log("Nearest: " + nearestInteractable.name);
        }

        for (int i = 0; i < interactables.Length; i++)
        {
            if (i != nearestIndex)
            {
                otherInteractables[index] = interactables[i];
                otherInteractables[index].GetComponent<DialogueTrigger>().enabled = false;
                index++;
            }
        }

        activeDialogueTrigger = nearestInteractable.GetComponent<DialogueTrigger>();
        activeDialogueTrigger.enabled = true;
    }

    IEnumerator SwitchSprites(Dialogue dialogue)
    {
        int index = spriteCount;
        while (index <= spriteCount)
        {
            spriteRight.GetComponent<Image>().sprite = dialogue.spritesRight[spriteCount - 1];
            spriteLeft.GetComponent<Image>().sprite = dialogue.spritesLeft[spriteCount - 1];
            yield return new WaitForSeconds(animationSpeed);
            spriteRight.GetComponent<Image>().sprite = dialogue.spritesRight[spriteCount - 2];
            spriteLeft.GetComponent<Image>().sprite = dialogue.spritesLeft[spriteCount - 2];
            yield return new WaitForSeconds(animationSpeed);
        }
    }

    IEnumerator TextAnimation(string text)
    {
        //dialogueText.text = sentences.Dequeue().ToString();
        dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textDelay);
        }
    }
}