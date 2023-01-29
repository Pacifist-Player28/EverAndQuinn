using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [HideInInspector] public bool canBeClicked = true;
    public float triggerDistance = 4.5f;
    [Space]
    public Sprite transparentSprite;
    public Dialogue dialogue;
    public UnityEvent clicked;
    public UnityEvent endOfDialogue;

    private GameObject player;

    [HideInInspector]
    public DialogueTrigger instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Update()
    {
        //OnDistanceShorter(triggerDistance);
        //Debug.Log("CanBeclickes is " + canBeClicked);
        for (int i = 0; i < dialogue.spritesLeft.Length ; i++)
        {
            if (dialogue.spritesLeft[i] == null) dialogue.spritesLeft[i] = transparentSprite;
            if (dialogue.spritesRight[i] == null) dialogue.spritesRight[i] = transparentSprite;
        }
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }

    public void DestroyThisTriggerDialogue()
    {
        Destroy(this);
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
        DialogueManager.instance.activeDialogueTrigger = instance;
        if (!enabled) return;
        if (Input.GetMouseButtonDown(0) && Vector2.Distance(GetComponent<Transform>().position, player.transform.position) < triggerDistance)
        {
            TriggerDialogue();
            clicked.Invoke();
        }
    }
}