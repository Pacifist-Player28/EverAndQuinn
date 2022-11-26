using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueClickable : MonoBehaviour
{
    DialogueTrigger dialogueTrigger;

    void Start()
    {
        dialogueTrigger = GetComponentInParent<DialogueTrigger>();
    }

    void OnMouseOver()
    {
        Debug.Log("Hovering");
        if (Input.GetMouseButtonDown(0) && dialogueTrigger.canBeClicked)
        {
            dialogueTrigger.TriggerDialogue();

            dialogueTrigger.talkingRange.enabled = false;

            Debug.Log("clicked on NPC");
            //change color to white
        }
    }
}
