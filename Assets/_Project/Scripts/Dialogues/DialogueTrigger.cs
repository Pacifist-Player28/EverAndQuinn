using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace DialogueSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class DialogueTrigger : MonoBehaviour
    {
        [HideInInspector] public DialogueTrigger instance;

        [SerializeField] float triggerDistance = 4.5f;
        [SerializeField] bool interactOnce = false;
        [Space]
        public string[] emotionRight;
        public string[] emotionLeft;
        public Dialogue dialogue;
        [SerializeField] UnityEvent clicked;
        public UnityEvent startOfDialogue;
        public UnityEvent endOfDialogue;

        bool dialogueStart = false;
        GameObject player;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, triggerDistance);
        }

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            Transform parentTransform = transform.parent;
            int childIndex = transform.GetSiblingIndex();

            gameObject.name = parentTransform.gameObject.name + "_dialogue_" + childIndex;

            player = PlayerMovementKeyboard.instance.gameObject;
            //emotionForSentence = new string[???];
            Array.Resize(ref emotionRight, dialogue.sentences.Length);
            Array.Resize(ref emotionLeft, dialogue.sentences.Length);
        }

        void OnMouseOver()
        {
            //make this trigger active

            if (!enabled) return;
            else if (Input.GetMouseButtonDown(0) && Vector2.Distance(GetComponent<Transform>().position, player.transform.position) < triggerDistance)
            {
                DialogueManager.instance.activeDialogueTrigger = instance;
                DialogueManager.instance.StartDialogue(dialogue);
                clicked.Invoke();
            }
        }

        public void DestroyTriggerDialogue()
        {
            if (interactOnce)
                Destroy(this);
            else return;
        }

        public void TriggerActive()
        {
            DialogueManager.instance.activeDialogueTrigger = this;
        }

        public void StartDialogeOverTime(float time)
        {
            if (!dialogueStart)
            {
                StartCoroutine(StartDialogueOverTime(time));
                dialogueStart = true;
            }
        }

        IEnumerator StartDialogueOverTime(float time)
        {
            TriggerActive();
            yield return new WaitForSeconds(time);
            DialogueManager.instance.StartDialogue(dialogue);
        }
    }
}