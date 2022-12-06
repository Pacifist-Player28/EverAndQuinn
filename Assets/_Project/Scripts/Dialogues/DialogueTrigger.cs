using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [HideInInspector] public bool canBeClicked = true;
    public float triggerDistance = 10f;
    public Dialogue dialogue;   


    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Update()
    {
        OnDistanceShorter(triggerDistance);
        Debug.Log("CanBeclickes is " + canBeClicked);
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void OnDistanceShorter(float distance)
    {
        if (Vector2.Distance(GetComponent<Transform>().position, player.transform.position) < distance)
        {
            Debug.Log("Distance is short enough");
            canBeClicked = true;
        }
        else canBeClicked = false;
    }

    void OnMouseOver()
    {
        //Debug.Log("Hovering");
        if (Input.GetMouseButton(0) && canBeClicked)
        {
            TriggerDialogue();
            GetComponent<Collider2D>().enabled = false;
        }
    }
}