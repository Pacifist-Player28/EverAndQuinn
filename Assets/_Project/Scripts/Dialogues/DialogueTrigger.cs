using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DialogueSystem
{ 
    public class DialogueTrigger : MonoBehaviour
    {
        [HideInInspector] public bool canBeClicked = true;
        public float triggerDistance = 4.5f;
        [Space]
        public Sprite transparentSprite;
        public Dialogue dialogue;
        public UnityEvent clicked;
        public UnityEvent endOfDialogue;

        private GameObject player;

        [HideInInspector]
        public DialogueTrigger instance;

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

        public void TriggerDialogeOverTime(float time)
        {
            StartCoroutine(StartDialogueOverTime(time));
        }

        public void MakeThisActive()
        {
            DialogueManager.instance.activeDialogueTrigger = instance;
        }

        IEnumerator StartDialogueOverTime(float time)
        {
            yield return new WaitForSeconds(time);
            DialogueManager.instance.StartDialogue(dialogue);
            
        }
    }
}