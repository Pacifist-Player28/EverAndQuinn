using UnityEngine;
using Inventory;
using UnityEngine.Events;
using DialogueSystem;
using TMPro;
using UnityEngine.Tilemaps;

public class GameSettings : MonoBehaviour
{
    [HideInInspector] public static GameSettings instance;
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

    [Header("Puzzles")]
    [SerializeField] AudioClip solvedPuzzleClip;
    [Space]
    public int solvedSlots_1;
    public int solvedMax_1;
    [Space]
    public int solvedSlots_2;
    public int solvedMax_2;
    [Space]
    public bool puzzleSolved_1 = false;
    public bool puzzleSolved_2 = false;

    [Space]

    [Header("Trash")]
    [SerializeField] int trashCurrentAmount;
    [SerializeField] int trashMaxAmount;
    [SerializeField] TMP_Text trashUi;
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

    bool config;

    void Awake()
    {
        DialogueManager dialogueManager = GetComponent<DialogueManager>();
        //Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

        if (instance == null) instance = this;
        else
        {
            Destroy(this);
        }

    }

    private void Start()
    {
        SortSprites();

        if (!skipFirstDialogue)
            startDialogue.Invoke();

        if (!activateDebugWindow)
            debugWindow.SetActive(false);

        trashList = GameObject.FindGameObjectsWithTag("Trash");
        trashMaxAmount = trashList.Length;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PrintMouseHit();
        }

        _DebugWindow();
        CompareSolution();
    }

    void PrintMouseHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Mouse is currently hitting: " + hit.collider.gameObject.name);
        }
    }


    private void _DebugWindow()
    {
        sentenceCount_text.text = "Sentence Count: " + DialogueManager.instance.sentenceCount.ToString();

        if (DialogueManager.instance.activeDialogueTrigger == null) activeDialogue_text.text = "Sentence Count: null";
        else activeDialogue_text.text = "Sentence Count: " + DialogueManager.instance.activeDialogueTrigger.name;

        //currentEmotion_Left.text = "Active emotion left: " + DialogueManager.instance.activeDialogueTrigger.emotionLeft[DialogueManager.instance.sentenceCount -1];
        //currentEmotion_Right.text = "Active emotion right: " + DialogueManager.instance.activeDialogueTrigger.emotionRight[DialogueManager.instance.sentenceCount -1];

        if (DialogueManager.instance.currentEmotionLeft != null)
            emotionLeft_text.text = "Emotion left: " + DialogueManager.instance.currentEmotionLeft.ToString();

        if (DialogueManager.instance.currentEmotionRight != null)
            emotionRight_text.text = "Emotion right: " + DialogueManager.instance.currentEmotionRight.ToString();
    }

    public void _AddPuzzleItem01(int difference)
    {
        solvedSlots_1 = solvedSlots_1 + difference;
    }

    public void _AddPuzzleItem02(int difference)
    {
        solvedSlots_2 = solvedSlots_2 + difference;
    }

    private void CompareSolution()
    {
        if (!puzzle1Check && solvedSlots_1 == solvedMax_1)
        {
            puzzleSolved_1 = true;
            puzzle01.Invoke();
            puzzle1Check = true;
        }

        if (!puzzle2Check && solvedSlots_2 == solvedMax_2)
        {
            puzzleSolved_2 = true;
            puzzle02.Invoke();
            puzzle2Check = true;
        }

        if (!config && puzzleSolved_1 && puzzleSolved_2)
        {
            finish.Invoke();
            config = true;
        }
    }

    public void _ActivateMovement()
    {
        PlayerMovementKeyboard.instance.enabled = true;
    }

    public void _DeactivateMovement()
    {
        PlayerMovementKeyboard.instance.enabled = false;
    }

    public void _AddItemToInventory(ItemSetting item)
    {
        item.collected = true;
    }

    public void _ActivateAllDialoguePhone()
    {
        bool config = false;

        if (skipPhone && !config)
        {
            foreach (DialogueTrigger dialogue in FindObjectsOfType<DialogueTrigger>())
            {
                dialogue.enabled = true;
            }
            config = true;
        }
        else return;
    }

    public void _AllDialogue(bool state)
    {
        foreach (DialogueTrigger dialogue in FindObjectsOfType<DialogueTrigger>())
        { 
            dialogue.enabled = state;
        }
    }

    public void _UpdateTrashUI()
    {
        Debug.Log("Collected Trash ");
        trashCurrentAmount = trashCurrentAmount + 1;
        trashUi.text = (trashCurrentAmount * 100 / trashMaxAmount).ToString() + "% Trash collected";
        Debug.Log("Updated trashUI");
    }

    public void _AllObjects(bool state)
    {
        foreach (ObjectTriggerScript trash in FindObjectsOfType<ObjectTriggerScript>())
        {
            if (trash.CompareTag("Trash"))
            {
                trash.enabled = state;
            }
        }
    }

    public void _AllItems(bool state)
    {
        foreach (Item item in FindObjectsOfType<Item>())
        {
            item.GetComponent<Collider2D>().enabled = state;
            item.enabled = state;
        }
    }

    public void _CollectedAllTrash()
    {
        if (trashCurrentAmount == trashMaxAmount && !trashCheck)
        {
            trashCollected.Invoke();
            trashCheck = true;
        }
    }

    public void SortSprites()
    {
        GameObject[] sortingObjects = GameObject.FindGameObjectsWithTag("LayerSorting");

        foreach (var sortedObject in sortingObjects)
        {
            SpriteRenderer renderer = sortedObject.GetComponent<SpriteRenderer>();
            renderer.sortingOrder = (int)renderer.transform.position.y;
        }
    }
}