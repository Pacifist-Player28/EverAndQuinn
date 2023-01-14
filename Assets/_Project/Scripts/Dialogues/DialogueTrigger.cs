using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [HideInInspector] public bool canBeClicked = true;
    public float triggerDistance = 2.45f;
    [Space]
    public Dialogue dialogue;
    public UnityEvent clicked;

    private GameObject player;
    private DialogueManager dialogueManager;

    void Start()
    {
        player = GameObject.Find("Player");
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void Update()
    {
        OnDistanceShorter(triggerDistance);
        //Debug.Log("CanBeclickes is " + canBeClicked);
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void OnDistanceShorter(float distance)
    {
        if (Vector2.Distance(GetComponent<Transform>().position, player.transform.position) < distance)
        {
            //Debug.Log("Distance is short enough");
            canBeClicked = true;
        }
        else canBeClicked = false;
    }

    void OnMouseOver()
    {
        if (!canBeClicked) return;
        if (Input.GetMouseButtonDown(0))
        {
            TriggerDialogue();
            clicked.Invoke();
        }
    }
}