using UnityEngine;
using System;
using Inventory;
using UnityEngine.Events;

public class GameSettings : MonoBehaviour
{
    [HideInInspector]
    public static GameSettings instance;

    [SerializeField]
    private Texture2D cursorTexture;

    public int slotsSolved_first;
    public int solution_first;
    [Space]
    public int slotsSolved_second;
    public int solution_second;
    [Space]
    public bool solved_first = false;
    public bool solved_second = false;
    [Space]
    [HideInInspector]
    public static GameSettings current;
    public event Action OnPlayerEnter;
    [Space]
    public UnityEvent puzzle01;
    public UnityEvent puzzle02;
    public UnityEvent finish;

    private bool puzzle1Check = false;
    private bool puzzle2Check = false;

    void Awake()
    {
        current = this;
        DialogueManager dialogueManager = GetComponent<DialogueManager>();

        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

        if (instance == null) instance = this;
        else Destroy(this);
    }

    void Update()
    {
        CompareSolution();
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

        if (slotsSolved_second == solution_second && slotsSolved_first == solution_first) finish.Invoke();
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
}


