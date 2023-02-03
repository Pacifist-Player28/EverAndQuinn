using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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
        GetComponent<Collider2D>().enabled = false;
    }

    public void DestroyThisTriggerDialogue()
    {
        Destroy(this);
    }

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

    public void TriggerDialogeOverTime(float time)
    {
        StartCoroutine(StartDialogueOverTime(time));
    }

    IEnumerator StartDialogueOverTime(float time)
    {
        yield return new WaitForSeconds(time);
        TriggerDialogue();
    }
}