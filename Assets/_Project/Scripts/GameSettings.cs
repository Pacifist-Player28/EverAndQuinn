using UnityEngine;
using System;

public class GameSettings : MonoBehaviour
{
    [HideInInspector]
    public static GameSettings instance;
    public int solutionSlotsSolved;
    public int solution;
    public bool puzzleSolved = false;

    public static GameSettings current;
    public event Action OnPlayerEnter;

    void Awake()
    {
        current = this;
        DialogueManager dialogueManager = GetComponent<DialogueManager>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
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

    public void AddSolutionPuzzle()
    {
        solutionSlotsSolved++;
    }

    public void SubtractSolutuionPuzzle()
    {
        solutionSlotsSolved--;
    }

    public void CompareSolution()
    {
        if(solutionSlotsSolved == solution)
        {
            Debug.Log("Solution found");
            puzzleSolved = true;
        }
    }
}


