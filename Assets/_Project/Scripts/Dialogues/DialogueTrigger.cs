using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DialogueSystem
{ 
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] bool canBeClicked = true;
        [SerializeField] float triggerDistance = 4.5f;
        [Space]
        [SerializeField] Sprite transparentSprite;
        [SerializeField] UnityEvent clicked;
        public string[] emotionRight;
        public string[] emotionLeft;
        public Dialogue dialogue;
        public UnityEvent endOfDialogue;
        public UnityEvent startOfDialogue;

        [HideInInspector]
        public DialogueTrigger instance;

        bool dialogueTriggered = false;
        GameObject player;

        private void Awake()
        {
            instance = this;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, triggerDistance);
        }

        void Start()
        {
            player = PlayerMovementKeyboard.instance.gameObject;
            //emotionForSentence = new string[???];
        }

        public void DestroyTriggerDialogue()
        {
            Destroy(this);
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

        public void MakeThisActive()
        {
            DialogueManager.instance.activeDialogueTrigger = this;
        }

        public void TriggerDialogeOverTime(float time)
        {
            if (!dialogueTriggered)
            {
                dialogueTriggered = true;
                StartCoroutine(StartDialogueOverTime(time));
            }
        }

        IEnumerator StartDialogueOverTime(float time)
        {
            MakeThisActive();
            yield return new WaitForSeconds(time);
            DialogueManager.instance.StartDialogue(dialogue);
        }
    }
}