using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    //from here out we need a way to start the dialogue: If player is in range AND clicks on NPC, the dialoge starts. Right now it starts
    //the conversation without clicking but by triggering the collider.
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player")) TriggerDialogue();
    }
}
