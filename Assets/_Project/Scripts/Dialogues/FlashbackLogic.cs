using UnityEngine;
using DialogueSystem;

public class FlashbackLogic : MonoBehaviour
{
    DialogueTrigger trigger;
    [SerializeField] int sentenceFlashback;
    [SerializeField] Animator animator;

    private void Start()
    {
        trigger = GetComponent<DialogueTrigger>();
    }

    private void Update()
    {
        if(DialogueManager.instance.sentenceCount == sentenceFlashback)
        {
            animator.Play("Flashback01");
        }
    }
}