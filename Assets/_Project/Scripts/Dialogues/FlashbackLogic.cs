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
            Debug.Log("Playing");
            animator.Play("FlashBack_01");
        }
    }
}