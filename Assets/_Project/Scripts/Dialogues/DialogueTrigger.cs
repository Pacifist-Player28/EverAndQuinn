using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [HideInInspector] public bool canBeClicked = true;
    public float triggerDistance = 4.5f;
    [Space]
    public bool destroy = false;
    public Dialogue dialogue;
    public UnityEvent clicked;
    public UnityEvent endOfDialogue;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Update()
    {
        //OnDistanceShorter(triggerDistance);
        //Debug.Log("CanBeclickes is " + canBeClicked);
    }

    public void TriggerDialogue()
    {
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

        DialogueManager.instance.StartDialogue(dialogue);
    }

    public void DestroyThisTriggerDialogue()
    {
        if (destroy == true)
        {
            triggerDistance = 0;
        }
    }

    //public void OnDistanceShorter(float distance)
    //{
    //    if (Vector2.Distance(GetComponent<Transform>().position, player.transform.position) < distance)
    //    {
    //        //Debug.Log("Distance is short enough");
    //        canBeClicked = true;
    //    }
    //    else canBeClicked = false;
    //}


    void OnMouseOver()
    {
        //if (!canBeClicked) return;
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //DialogueManager.instance.activeTrigger = this;
        //    TriggerDialogue();
        //    clicked.Invoke();
        //}

        if (!enabled) return;
        if (Input.GetMouseButtonDown(0) && Vector2.Distance(GetComponent<Transform>().position, player.transform.position) < triggerDistance)
        {
            TriggerDialogue();
            clicked.Invoke();
        }
    }
}