using UnityEngine;
using System;
using Inventory;
using UnityEngine.Events;
using DialogueSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class GameSettings : MonoBehaviour
{
    [HideInInspector]
    public static GameSettings instance;
    //[SerializeField] Texture2D cursorTexture;

    public int slotsSolved_first;
    public int solution_first;
    [Space]
    public int slotsSolved_second;
    public int solution_second;
    [Space]
    public bool solved_trash = false;
    public bool solved_first = false;
    public bool solved_second = false;
    [Space]
    [SerializeField] GameObject trashSlider;
    [SerializeField] AudioClip trashFullAudio;
    [HideInInspector]
    public static GameSettings current;
    public event Action OnPlayerEnter;
    [Space]
    public UnityEvent startDialogue;
    public UnityEvent puzzle01;
    public UnityEvent puzzle02;
    public UnityEvent finish;
    public UnityEvent trashCollected;

    private bool puzzle1Check = false;
    private bool puzzle2Check = false;

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
        startDialogue.Invoke();
    }

    void Update()
    {
        CompareSolution();
        if(trashSlider.GetComponent<Slider>().value == trashSlider.GetComponent<Slider>().maxValue)
        {
            solved_trash = true;
            trashCollected.Invoke();
        }
    }

    //

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

    public void ActivateAllDialogue()
    {
        DialogueTrigger[] dialogues = FindObjectsOfType<DialogueTrigger>();
        foreach (var dialogue in dialogues)
        {
            dialogue.enabled = true;
        }
    }

    public void ChangeSlider()
    {
        var slider = trashSlider.GetComponent<Slider>();
        slider.value = slider.value + 1;
        trashSlider.gameObject.GetComponentInChildren<TMP_Text>().text = ((slider.value / slider.maxValue)*100).ToString("0") + "% trash collected";
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
}