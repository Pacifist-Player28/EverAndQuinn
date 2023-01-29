using UnityEngine;
using UnityEngine.Events;

public class CutSceneTrigger : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnCollision;

    private void Start()
    {
        
    }

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