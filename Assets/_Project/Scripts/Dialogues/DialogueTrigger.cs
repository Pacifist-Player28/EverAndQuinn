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
    private Collider2D colliderOfObject;

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
            //Debug.Log("Distance is short enough");
            canBeClicked = true;
        }
        else canBeClicked = false;
    }

    void DeactivateTriggerDialogue()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Interactable");
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name != name)
                gameObject.GetComponent<DialogueTrigger>().enabled = false;
        }
    }

    void ActivateTriggerDialogue()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Interactable");
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name != name)
                gameObject.GetComponent<DialogueTrigger>().enabled = true;
        }
    }

    void OnMouseOver()
    {
        Debug.Log("Hovering");
        if (Input.GetMouseButtonDown(0) && canBeClicked)
        {
            TriggerDialogue();

            //colliderOfObject.enabled = false;

            clicked.Invoke();

            Debug.Log("clicked on dialogue object");
            //change color to white
        }
    }

    private void OnMouseDown()
    {
        DeactivateTriggerDialogue();
    }



}