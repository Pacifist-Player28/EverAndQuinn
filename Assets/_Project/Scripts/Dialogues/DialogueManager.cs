using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using static DialogueSystem.DialogueTrigger;

namespace DialogueSystem 
{ 
    public class DialogueManager : MonoBehaviour
    {
        [HideInInspector] public static DialogueManager instance;
        [HideInInspector] public DialogueTrigger activeDialogueTrigger;
        PlayerMovementKeyboard player;

        [Header("Dialogue Settings")]
        [SerializeField] TMP_Text dialogueText;
        [SerializeField] AudioClip textSoundMain;
        [SerializeField] float textDelay = 0.01f;
        [Space]
        [SerializeField] EmotionList emotionList;
        [Space]
        [SerializeField] GameObject dialogueUi;
        [SerializeField] GameObject spriteLeft, spriteRight;
        [SerializeField] float spriteAnimationSpeed;
        public string currentEmotionLeft, currentEmotionRight;

        GameObject[] interactables;
        Queue<string> sentences;
        [HideInInspector] public int sentenceCount = 0;
        AudioSource audioSource;

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
            player = PlayerMovementKeyboard.instance;
        }

        private void Update()
        {
            //Debug.Log("emotion for sentence right: " + currentEmotionRight + " emotion for sentence left: " + currentEmotionLeft);
            //Debug.Log("Sentence count: " + sentenceCount);
            //Debug.Log("SpriteCount: " + spriteCount);
            if (Input.GetKeyDown(KeyCode.Space) && dialogueUi.activeSelf == true) DisplayNextSentence();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            player.animator.Play(player.idleFront);
            sentences.Clear();
            dialogueUi.SetActive(true);
            activeDialogueTrigger.startOfDialogue.Invoke();
            player.enabled = false;

            ChangeInteractableColliderState(false);

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
            ChangeSpriteRight();
            ChangeSpriteLeft();
        }

        public void EndDialogue()
        {
            StopAllCoroutines();
            sentenceCount -= sentenceCount;
            activeDialogueTrigger.endOfDialogue.Invoke();
            activeDialogueTrigger._DestroyTriggerDialogue();

            ChangeInteractableColliderState(true);

            //if (activeDialogueTrigger.GetComponent<Collider2D>() == null) return;
            dialogueUi.SetActive(false);
            player.enabled = true;
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
            SetCurrentEmotions();

            StartCoroutine(SwitchAndReplaceSprites(currentEmotionRight, spriteRight.GetComponent<Image>()));
        }

        public void ChangeSpriteLeft()
        {
            SetCurrentEmotions();

            StartCoroutine(SwitchAndReplaceSprites(currentEmotionLeft, spriteLeft.GetComponent<Image>()));
        }

        private void SetCurrentEmotions()
        {
            if (sentenceCount > 0)
            {
                currentEmotionRight = activeDialogueTrigger.emotionRight[sentenceCount - 1];
                currentEmotionLeft = activeDialogueTrigger.emotionLeft[sentenceCount - 1];
            }
        }

        private IEnumerator SwitchAndReplaceSprites(string currentEmotion, Image sprite)
        {
            if (currentEmotion == emotionList.string_transparent || currentEmotion == null)
            {
                sprite.sprite = emotionList.transparentSprite;
            }
            else if (emotionList.spriteDictionary.TryGetValue(currentEmotion, out var spriteArray))
            {
                while (true)
                {
                    sprite.sprite = spriteArray[0];
                    yield return new WaitForSeconds(spriteAnimationSpeed);
                    sprite.sprite = spriteArray[1];
                    yield return new WaitForSeconds(spriteAnimationSpeed);
                }
            }
            else
            {
                var text = GameSettings.instance.foundSprite;
                text.text = "NO SPRITE FOUND";
                text.color = Color.red;
            }
        }

        private void ChangeInteractableColliderState(bool state)
        {
            foreach (GameObject interactable in GameObject.FindGameObjectsWithTag("Interactable"))
            {
                interactable.GetComponent<Collider2D>().enabled = state;
            }
        }

        public void _DeactivateDialogue(string optionTodeactivate)
        {
            var allTriggers = Resources.FindObjectsOfTypeAll<DialogueTrigger>();

            foreach (var trigger in allTriggers)
            {
                if (trigger.dialogueTag.ToString() == optionTodeactivate)
                {
                    trigger.gameObject.SetActive(false);
                }
            }
        }

        public void _ActivateDialogue(string optionTodeactivate)
        {
            var allTriggers = Resources.FindObjectsOfTypeAll<DialogueTrigger>();

            foreach (var trigger in allTriggers)
            {
                if (trigger.dialogueTag.ToString() == optionTodeactivate)
                {
                    trigger.gameObject.SetActive(true);
                }
            }
        }
    }
}