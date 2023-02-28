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
        [SerializeField] Emotions emotions;
        [SerializeField] GameObject spriteLeft, spriteRight;
        public string currentEmotionLeft, currentEmotionRight;
        [Space]
        [SerializeField] float spriteAnimationSpeed;
        GameObject[] interactables;
        Queue<string> sentences;
        int sentenceCount = 0;
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
            sentenceCount = 0;
            sentences = new Queue<string>();
            playerMovement = PlayerMovementKeyboard.instance;
            interactables = GameObject.FindGameObjectsWithTag("Interactable");
        }

        private void Update()
        {
            //Debug.Log("emotion for sentence right: " + currentEmotionRight + " emotion for sentence left: " + currentEmotionLeft);
            Debug.Log("Sentence count: " + sentenceCount);
            //Debug.Log("SpriteCount: " + spriteCount);
            if (Input.GetKeyDown(KeyCode.Space) && dialogueUi.activeSelf == true) DisplayNextSentence();

            if (sentenceCount <= 0) return;
            else 
            { 
                currentEmotionRight = activeDialogueTrigger.emotionRight[sentenceCount - 1];
                currentEmotionLeft = activeDialogueTrigger.emotionLeft[sentenceCount - 1];
            }

            var index = sentenceCount;
            if(index != sentenceCount)
            {
                ChangeSpriteRight();
                ChangeSpriteLeft();
            }

        }

        public void StartDialogue(Dialogue dialogue)
        {
            playerMovement.animator.Play(playerMovement.idleFront);
            sentences.Clear();
            dialogueUi.SetActive(true);
            playerMovement.enabled = false;
            activeDialogueTrigger.startOfDialogue.Invoke();

            for (int i = 0; i < interactables.Length; i++)
            {
                //Debug.Log("name: " + interactables[i].name);
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
            sentenceCount += 1;
            StartCoroutine(TextAnimation(sentences.Dequeue().ToString()));
        }

        public void EndDialogue()
        {
            StopAllCoroutines();
            sentenceCount -= sentenceCount;
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

        public void ChangeSpriteRight()
        {
            if(currentEmotionRight == "transparent" || currentEmotionRight == null)
            {
                var sprite = emotions.transparent;
                StartCoroutine(SwitchAndReplaceRightSprites(sprite, sprite));
            }
            else if(currentEmotionRight == "QuinnNeutral")
            {
                var sprite1 = emotions.quinn_neutral[0];
                var sprite2 = emotions.quinn_neutral[1];
                StartCoroutine(SwitchAndReplaceRightSprites(sprite1, sprite2));
            }
            else if (currentEmotionRight == "QuinnAngry")
            {
                var sprite1 = emotions.quinn_angry[0];
                var sprite2 = emotions.quinn_angry[1];
                StartCoroutine(SwitchAndReplaceRightSprites(sprite1, sprite2));
            }
            else if (currentEmotionRight == "QuinnConfused")
            {
                var sprite1 = emotions.quinn_confused[0];
                var sprite2 = emotions.quinn_confused[1];
                StartCoroutine(SwitchAndReplaceRightSprites(sprite1, sprite2));
            }
            else if (currentEmotionRight == "QuinnNervous")
            {
                var sprite1 = emotions.quinn_nervous[0];
                var sprite2 = emotions.quinn_nervous[1];
                StartCoroutine(SwitchAndReplaceRightSprites(sprite1, sprite2));
            }
            else if(currentEmotionRight == "QuinnStop")
            {
                var sprite1 = emotions.quinn_stop;
                StartCoroutine(SwitchAndReplaceRightSprites(sprite1, sprite1));
            }
            else if(currentEmotionRight == "QuinnSurprised")
            {
                var sprite1 = emotions.quinn_surprised[0];
                var sprite2 = emotions.quinn_surprised[1];
                StartCoroutine(SwitchAndReplaceRightSprites(sprite1, sprite2));
            }
        }

        public void ChangeSpriteLeft()
        {
            if (currentEmotionLeft == "transparent" || currentEmotionLeft == null)
            {
                var sprite1 = emotions.transparent;
                StartCoroutine(SwitchAndReplaceLeftSprites(sprite1, sprite1));
            }
            else if (currentEmotionLeft == "CarolynNeutral")
            {
                var sprite1 = emotions.carolyn_neutral[0];
                var sprite2 = emotions.carolyn_neutral[1];
                StartCoroutine(SwitchAndReplaceLeftSprites(sprite1, sprite2));
            }
            else if (currentEmotionLeft == "CarolynStop")
            {
                var sprite1 = emotions.carolyn_stop;
                StartCoroutine(SwitchAndReplaceLeftSprites(sprite1, sprite1));
            }
        }

        IEnumerator SwitchAndReplaceRightSprites(Sprite sprite1, Sprite sprite2)
        {
            //var spriteOnRight = spriteRight.GetComponent<Image>().sprite;
            //Debug.Log(spriteRight.GetComponent<Image>().sprite);
            var index = sentenceCount;
            while(index <= sentenceCount) 
            { 
                spriteRight.GetComponent<Image>().sprite = sprite1;
                yield return new WaitForSeconds(spriteAnimationSpeed);
                spriteRight.GetComponent<Image>().sprite = sprite2;
                yield return new WaitForSeconds(spriteAnimationSpeed);
            }
        }

        IEnumerator SwitchAndReplaceLeftSprites(Sprite sprite1, Sprite sprite2)
        {
            //var spriteOnRight = spriteRight.GetComponent<Image>().sprite;
            //Debug.Log(spriteRight.GetComponent<Image>().sprite);
            var index = sentenceCount;
            while (index <= sentenceCount)
            {
                spriteLeft.GetComponent<Image>().sprite = sprite1;
                yield return new WaitForSeconds(spriteAnimationSpeed);
                spriteLeft.GetComponent<Image>().sprite = sprite2;
                yield return new WaitForSeconds(spriteAnimationSpeed);
            }
        }
    }
}