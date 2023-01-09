using UnityEngine;
using System;

public class GameSettings : MonoBehaviour
{
    public int solutionSlotsSolved;
    public int solution;
    public bool puzzleSolved = false;

    public static GameSettings current;
    public event Action OnPlayerEnter;

    void Awake()
    {
        current = this;
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.Raycast(ray, Vector2.zero);
        }

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

    public void CompareSolution()
    {
        if(solutionSlotsSolved == solution)
        {
            Debug.Log("Solution found");
            puzzleSolved = true;
        }
    }
}


