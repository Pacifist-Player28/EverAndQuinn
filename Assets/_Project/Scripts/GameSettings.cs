using UnityEngine;
using System;

public class GameSettings : MonoBehaviour
{
    [HideInInspector]
    public static GameSettings instance;

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

    void Awake()
    {
        current = this;
        DialogueManager dialogueManager = GetComponent<DialogueManager>();

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
        if(slotsSolved_first == solution_first)
        {
            Debug.Log("Solution found for the first puzzle");
            solved_first = true;
        }

        if(slotsSolved_second == solution_second)
        {
            Debug.Log("Solution found for the first puzzle");
            solved_second = true;
        }
    }
}


