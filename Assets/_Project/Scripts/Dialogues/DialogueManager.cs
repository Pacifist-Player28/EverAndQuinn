using UnityEngine;
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
    public AudioClip textSound;
    public float timeBetweenTextSound;
    public GameObject spriteLeft, spriteRight;
    [Space]
    public float animationSpeed;
    public float textDelay = 0.01f;
    public GameObject[] interactables;
    public Vector2[] distanceToInteractables;

    private Queue<string> sentences;
    private PlayerMovementKeyboard playerMovement;

    private int spriteCount = 0;
    private AudioSource source;

    [HideInInspector]
    public static DialogueManager instance;
    [HideInInspector]
    public DialogueTrigger activeDialogueTrigger;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
        spriteCount = 0;
        sentences = new Queue<string>();
        //playerMovement = FindObjectOfType<PlayerMovementKeyboard>();
        playerMovement = PlayerMovementKeyboard.instance;
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
    }

    private void Update()
    {
        //MeasureAndActivate();
        //Debug.Log("active dialogue: " + activeDialogueTrigger.name);
        if (Input.GetKeyDown(KeyCode.Space) && dialogueUi.activeSelf == true) DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        playerMovement.animator.Play(playerMovement.idleFront);
        sentences.Clear();
        //enable UI
        dialogueUi.SetActive(true);
        //disable Player movement
        playerMovement.enabled = false;

        for (int i = 0; i < interactables.Length; i++)
        {
            interactables[i].GetComponent<Collider2D>().enabled = false;
        }

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        StopAllCoroutines();
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        spriteCount += 2;
        StartCoroutine(TextAnimation(sentences.Dequeue().ToString()));
        DisplayNextSprites(activeDialogueTrigger.dialogue);

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
        for (int i = 0; i < interactables.Length; i++)
        {
            interactables[i].GetComponent<Collider2D>().enabled = true;
        }

        //disable UI
        dialogueUi.SetActive(false);

        //enable Player movement
        playerMovement.enabled = true;

        spriteCount -= spriteCount;

        StopAllCoroutines();

        activeDialogueTrigger.endOfDialogue.Invoke();
    }

    public void AddItemToInventory(ItemSetting item)
    {
        item.collected = true;
    }

    //public void MeasureAndActivate()
    //{
    //    //this method activates the nearest interactable and disables every other inside the scene.
    //    distanceToInteractables = new Vector2[interactables.Length];
    //    Vector2 smallestVector = new Vector2(float.MaxValue, float.MaxValue);
    //    GameObject nearestInteractable = null;
    //    GameObject[] otherInteractables = new GameObject[interactables.Length - 1];
    //    int nearestIndex = -1;
    //    int index = 0;

    //    for (int i = 0; i < interactables.Length; i++)
    //    {
    //        distanceToInteractables[i] = interactables[i].transform.position - playerMovement.transform.position;
    //    }

    //    for (int i = 0; i < distanceToInteractables.Length; i++)
    //    {
    //        if (distanceToInteractables[i].magnitude < smallestVector.magnitude)
    //        {
    //            smallestVector = distanceToInteractables[i];
    //            nearestIndex = i;
    //        }
    //    }

    //    if (nearestIndex != -1)
    //    {
    //        nearestInteractable = interactables[nearestIndex];
    //        //Debug.Log("Nearest: " + nearestInteractable.name);
    //    }

    //    for (int i = 0; i < interactables.Length; i++)
    //    {
    //        if (i != nearestIndex)
    //        {
    //            otherInteractables[index] = interactables[i];
    //            otherInteractables[index].GetComponent<DialogueTrigger>().enabled = false;
    //            index++;
    //        }
    //    }
    //    activeDialogueTrigger = nearestInteractable.GetComponent<DialogueTrigger>();
    //    activeDialogueTrigger.enabled = true;
    //}

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
        int soundIndex = 0;

        foreach (char letter in text.ToCharArray())
        {
            soundIndex++;
            if (soundIndex == 3)
            {
                source.PlayOneShot(textSound);
                soundIndex -= soundIndex;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(textDelay);
        }
    }
}