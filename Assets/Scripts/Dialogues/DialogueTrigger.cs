using UnityEngine;
using System;

public class DialogueTrigger : MonoBehaviour
{
    [HideInInspector] public bool canBeClicked = false;
    //[HideInInspector] 
    public Collider2D talkingRange;
    public Dialogue dialogue;

    void Start()
    {
        //talkingRange = GetComponent<Collider2D>();
    }

    public void Update()
    {
        Debug.Log("CanBeclickes is " + canBeClicked);
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //player is inside the trigger and the player can click the NPC
        if (other.CompareTag("Player")) canBeClicked = true;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) canBeClicked = false;
    }


}
