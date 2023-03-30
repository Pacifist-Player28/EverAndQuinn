using UnityEngine;
using DialogueSystem;

public class FlashbackLogic : MonoBehaviour
{
    DialogueTrigger trigger;
    [SerializeField] int sentenceFlashback;
    [SerializeField] Animator animator;

    bool collision = false;

    private void Start()
    {
        trigger = GetComponent<DialogueTrigger>();
    }

    private void Update()
    {
        if(sentenceFlashback == 0)
        {
            if (collision) animator.Play("FlashBack_02");
        }
        else
        {
            if (DialogueManager.instance.sentenceCount == sentenceFlashback) animator.Play("FlashBack_01");
            else return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.GetComponent<Collider2D>() != null)
        {
            if (other.CompareTag("Player")) collision = true;
        }
        else return;
    }
}