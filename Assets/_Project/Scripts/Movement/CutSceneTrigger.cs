using UnityEngine;
using UnityEngine.Events;
using DialogueSystem;

public class CutSceneTrigger : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnCollision;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var theDialogue = GetComponent<DialogueTrigger>().dialogue;
        if (collision.gameObject == PlayerMovementKeyboard.instance.gameObject)
        {
            OnCollision.Invoke();
            DialogueManager.instance.StartDialogue(theDialogue);
        }
    }
}