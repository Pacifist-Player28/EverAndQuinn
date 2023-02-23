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

        public void Update()
        {
            //OnDistanceShorter(triggerDistance);
            //Debug.Log("CanBeclickes is " + canBeClicked);
            for (int i = 0; i < dialogue.spritesLeft.Length ; i++)
            {
                if (dialogue.spritesLeft[i] == null) dialogue.spritesLeft[i] = transparentSprite;
                if (dialogue.spritesRight[i] == null) dialogue.spritesRight[i] = transparentSprite;
            }
            //emotionForSentence = new string[dialogue.sentences.Length];
        }

        public void DestroyThisTriggerDialogue()
        {
            Destroy(this);
        }

        void OnMouseOver()
        {
            //make this trigger active
            DialogueManager.instance.activeDialogueTrigger = instance;
            if (!enabled) return;
            if (Input.GetMouseButtonDown(0) && Vector2.Distance(GetComponent<Transform>().position, player.transform.position) < triggerDistance)
            {
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
            yield return new WaitForSeconds(time);
            DialogueManager.instance.StartDialogue(dialogue);
            yield return null;
        }

    }
}