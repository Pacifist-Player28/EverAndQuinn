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
        if (DialogueManager.instance.sentenceCount == sentenceFlashback)
        {
            Debug.Log("Playing");
            animator.Play("FlashBack_01");
        }
        else if (collision) animator.Play("FlashBack_02");
        else return;
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