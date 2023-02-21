using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;

namespace DialogueSystem 
{ 
    public class DialogueManager : MonoBehaviour
    {
        [Header("Dialogues")]
        [SerializeField] TMP_Text dialogueText;
        [SerializeField] AudioClip textSoundMain;
        [SerializeField] AudioClip textSoundPhone;
        [SerializeField] float textDelay = 0.01f;
        [SerializeField] GameObject dialogueUi;
        [SerializeField] GameObject spriteLeft, spriteRight;
        [Space]
        [SerializeField] float spriteAnimationSpeed;
        GameObject[] interactables;
        //SerializeField] Vector2[] distanceToInteractables;

        Queue<string> sentences;

        int spriteCount = 2;
        AudioSource audioSource;

        [HideInInspector] public static DialogueManager instance;
        [HideInInspector] public DialogueTrigger activeDialogueTrigger;
        PlayerMovementKeyboard playerMovement;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(this);
        }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            spriteCount = 0;
            sentences = new Queue<string>();
            playerMovement = PlayerMovementKeyboard.instance;
            interactables = GameObject.FindGameObjectsWithTag("Interactable");
        }

        private void Update()
        {
            //Debug.Log("SpriteCount: " + spriteCount);
            if (Input.GetKeyDown(KeyCode.Space) && dialogueUi.activeSelf == true) DisplayNextSentence();
            if (dialogueText.ToString().Contains("Ever")) Debug.Log("EVER");
        }

        public void StartDialogue(Dialogue dialogue)
        {
            playerMovement.animator.Play(playerMovement.idleFront);
            sentences.Clear();
            dialogueUi.SetActive(true);
            playerMovement.enabled = false;
            //spriteCount += 2;

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
            StartCoroutine(SwitchSprites(activeDialogueTrigger.dialogue));
        }

        public void EndDialogue()
        {
            StopAllCoroutines();
            spriteCount -= spriteCount;
            activeDialogueTrigger.endOfDialogue.Invoke();
            for (int i = 0; i < interactables.Length; i++)
            {
                interactables[i].GetComponent<Collider2D>().enabled = true;
            }

            //if (activeDialogueTrigger.GetComponent<Collider2D>() == null) return;
            dialogueUi.SetActive(false);
            playerMovement.enabled = true;
            //Debug.Log("ENDOFDIALOGUE");
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
            //Debug.Log("index: " + index + " spritecount: " + spriteCount);
            while (index <= spriteCount)
            {
                //Debug.Log("Dialogue Sprites: index = " + index + " spriteCount = " + spriteCount);
                spriteRight.GetComponent<Image>().sprite = dialogue.spritesRight[index - 1];
                spriteLeft.GetComponent<Image>().sprite = dialogue.spritesLeft[index - 1];
                yield return new WaitForSeconds(spriteAnimationSpeed);
                spriteRight.GetComponent<Image>().sprite = dialogue.spritesRight[index - 2];
                spriteLeft.GetComponent<Image>().sprite = dialogue.spritesLeft[index - 2];
                yield return new WaitForSeconds(spriteAnimationSpeed);
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
                    audioSource.PlayOneShot(textSoundMain);
                    soundIndex -= soundIndex;
                }
                dialogueText.text += letter;
                yield return new WaitForSeconds(textDelay);
            }
        }
    }
}