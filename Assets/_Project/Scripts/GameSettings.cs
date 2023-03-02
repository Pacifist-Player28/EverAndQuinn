using UnityEngine;
using System;
using Inventory;
using UnityEngine.Events;
using DialogueSystem;
using UnityEngine.UI;
using TMPro;

public class GameSettings : MonoBehaviour
{
    [HideInInspector]
    public static GameSettings instance;
    //[SerializeField] Texture2D cursorTexture;
    [Header("Debugging")]
    [SerializeField] bool skipFirstDialogue;
    [SerializeField] bool skipPhone;
    [SerializeField] bool activateDebugWindow;
    [SerializeField] GameObject debugWindow;
    public TMP_Text sentenceCount_text;
    public TMP_Text activeDialogue_text;
    public TMP_Text currentEmotion_Left;
    public TMP_Text currentEmotion_Right;
    public TMP_Text foundSprite;
    public TMP_Text emotionRight_text;
    public TMP_Text emotionLeft_text;
    [Space]
    [Header("Puzzle Info")]
    public int slotsSolved_first;
    public int solution_first;
    [Space]
    public int slotsSolved_second;
    public int solution_second;
    [Space]
    //public bool solved_trashPuzzle = false;
    public bool solved_first = false;
    public bool solved_second = false;
    [Space]
    [SerializeField] int trashAmount;
    [SerializeField] int trashGoalAmount;
    [SerializeField] TMP_Text trashUi;
    [SerializeField] AudioClip solvedPuzzleClip;
    [HideInInspector]
    public static GameSettings current;
    public event Action OnPlayerEnter;
    [Space]
    public UnityEvent startDialogue;
    public UnityEvent puzzle01;
    public UnityEvent puzzle02;
    public UnityEvent finish;
    public UnityEvent trashCollected;

    GameObject[] trashList;

    private bool puzzle1Check = false;
    private bool puzzle2Check = false;
    private bool trashCheck = false;

    bool invokedCheck;

    void Awake()
    {
        current = this;
        DialogueManager dialogueManager = GetComponent<DialogueManager>();
        DeactivateAllTrash();
        //Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        if(!skipFirstDialogue)
            startDialogue.Invoke();

        if (!activateDebugWindow)
            debugWindow.SetActive(false);

        trashList = GameObject.FindGameObjectsWithTag("Trash");
        trashGoalAmount = trashList.Length;
    }

    void Update()
    {
        sentenceCount_text.text = "Sentence Count: " + DialogueManager.instance.sentenceCount.ToString();
        if (DialogueManager.instance.activeDialogueTrigger == null) activeDialogue_text.text = "Sentence Count: null";
        else activeDialogue_text.text = "Sentence Count: " + DialogueManager.instance.activeDialogueTrigger.name;
        //currentEmotion_Left.text = "Active emotion left: " + DialogueManager.instance.activeDialogueTrigger.emotionLeft[DialogueManager.instance.sentenceCount -1];
        //currentEmotion_Right.text = "Active emotion right: " + DialogueManager.instance.activeDialogueTrigger.emotionRight[DialogueManager.instance.sentenceCount -1];
        if(DialogueManager.instance.currentEmotionLeft != null)
            emotionLeft_text.text = "Emotion left: " + DialogueManager.instance.currentEmotionLeft.ToString();
        if (DialogueManager.instance.currentEmotionRight != null)
            emotionRight_text.text = "Emotion right: " + DialogueManager.instance.currentEmotionRight.ToString();
        CompareSolution();
        if(trashAmount == trashGoalAmount && !trashCheck)
        {
            //solved_trashPuzzle = true;
            trashCollected.Invoke();
            trashCheck = true;
        }
    }

    public void PlayerEnter()
    {
        if (OnPlayerEnter != null)
        {
            OnPlayerEnter();
        }
    }

    public void AddSolutionPuzzle01()
    {
        slotsSolved_first++;
    }

    public void SubtractSolutuionPuzzle01()
    {
        slotsSolved_first--;
    }

    public void AddSolutionPuzzle02()
    {
        slotsSolved_second++;
    }

    public void SubtractSolutuionPuzzle02()
    {
        slotsSolved_second--;
    }

    public void CompareSolution()
    {
        if(slotsSolved_first == solution_first && !puzzle1Check)
        {
            Debug.Log("Solution found for the first puzzle");
            solved_first = true;
            puzzle01.Invoke();
            puzzle1Check = true;
        }

        if(slotsSolved_second == solution_second && !puzzle2Check)
        {
            Debug.Log("Solution found for the first puzzle");
            solved_second = true;
            puzzle02.Invoke();
            puzzle2Check = true;
        }

        if (slotsSolved_second == solution_second && slotsSolved_first == solution_first && !invokedCheck)
        {
            finish.Invoke();
            invokedCheck = true;
        }
    }

    public void ActivateMovement()
    {
        PlayerMovementKeyboard.instance.enabled = true;
    }

    public void DeactivateMovement()
    {
        PlayerMovementKeyboard.instance.enabled = false;
    }

    public void AddItemToInventory(ItemSetting item)
    {
        item.collected = true;
    }

    public void ActivateAllDialoguePhone()
    {
        bool config = false;

        if (skipPhone && !config)
        {
            DialogueTrigger[] dialogues = FindObjectsOfType<DialogueTrigger>();
            foreach (DialogueTrigger dialogue in dialogues)
            {
                dialogue.enabled = true;
            }
            config = true;
        }
        else return;
    }

    public void ActivateAllDialogue()
    {
        DialogueTrigger[] dialogues = FindObjectsOfType<DialogueTrigger>();
        foreach (DialogueTrigger dialogue in dialogues)
        { 
            dialogue.enabled = true;
        }
    }

    public void AddTrash()
    {
        Debug.Log("Collected Trash ");
        trashAmount = trashAmount + 1;
        trashUi.text = (trashAmount * 100 / trashGoalAmount).ToString() + "% Trash collected";
        Debug.Log("Updated trashUI");
    }

    public void ActivateAllTrash()
    {
        ObjectTriggerScript[] trash = FindObjectsOfType<ObjectTriggerScript>();

        for (int i = 0; i < trash.Length; i++)
        {
            if (trash[i].CompareTag("Trash"))
            {
                trash[i].enabled = true;
            }
        }
    }

    public void DeactivateAllTrash()
    {
        ObjectTriggerScript[] trash = FindObjectsOfType<ObjectTriggerScript>();

        for (int i = 0; i < trash.Length; i++)
        {
            if (trash[i].CompareTag("Trash"))
            {
                trash[i].enabled = false;
            }
        }
    }

    public void ActivateALlItems()
    {
        Item[] lol = FindObjectsOfType<Item>();

        for (int i = 0; i < lol.Length; i++)
        {
            lol[i].GetComponent<Collider2D>().enabled = true;
        }
    }

    public void DisableAllItems()
    {
        Item[] lol = FindObjectsOfType<Item>();

        for (int i = 0; i < lol.Length; i++)
        {
            lol[i].GetComponent<Collider2D>().enabled = false;
        }
    }


    public void DebugMessage(string message)
    {
        Debug.Log(message);
    }
}