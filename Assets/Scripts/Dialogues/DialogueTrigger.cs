using UnityEngine;
using System;

public class DialogueTrigger : MonoBehaviour
{
    [HideInInspector] public bool canBeClicked = false;
    public float triggerDistance = 1f;
    public Dialogue dialogue;

    private GameObject player;
    private Collider2D colliderOfThisObject;

    void Start()
    {
        player = GameObject.Find("Player");
        colliderOfThisObject = GetComponentInChildren<Collider2D>();
    }

    public void Update()
    {
        Debug.Log("CanBeclickes is " + canBeClicked);
        OnDistanceShorter(triggerDistance);
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

  /*public void OnTriggerEnter2D(Collider2D other)
    {
        //player is inside the trigger and the player can click the NPC
        if (other.CompareTag("Player")) canBeClicked = true;
    } */

    public void OnDistanceShorter(float distance)
    {
        if (Vector2.Distance(GetComponent<Transform>().position, player.transform.position) <= distance)
        {
            Debug.Log("Distance is short enough");
            canBeClicked = true;
        }
        else canBeClicked = false;
    }

    /* public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) canBeClicked = false;
    } */


}
