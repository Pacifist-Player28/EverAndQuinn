using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [HideInInspector] public bool canBeClicked = true;
    public float triggerDistance = 2.45f;
    [Space]
    public AnimationCurve curve;
    public Dialogue dialogue;
    public UnityEvent clicked;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Update()
    {
        OnDistanceShorter(triggerDistance);
        //Debug.Log("CanBeclickes is " + canBeClicked);
    }

    public void TriggerDialogue()
    {
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

        DialogueManager.instance.StartDialogue(dialogue);
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
            //DialogueManager.instance.activeTrigger = this;
            TriggerDialogue();
            clicked.Invoke();
        }
    }
}