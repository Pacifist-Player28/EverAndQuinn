using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueClickable : MonoBehaviour
{
    DialogueTrigger dialogueTrigger;
    private Collider2D colliderOfObject;

    void Start()
    {
        dialogueTrigger = GetComponentInParent<DialogueTrigger>();
        colliderOfObject = GetComponent<Collider2D>();
    }

    void OnMouseOver()
    {
        Debug.Log("Hovering");
        if (Input.GetMouseButtonDown(0) && dialogueTrigger.canBeClicked)
        {
            dialogueTrigger.TriggerDialogue();

            colliderOfObject.enabled = false;

            Debug.Log("clicked on NPC");
            //change color to white
        }
    }
}
