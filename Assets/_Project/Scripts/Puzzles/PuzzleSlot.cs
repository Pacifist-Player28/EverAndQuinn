using UnityEngine;
using UnityEngine.Events;

public class PuzzleSlot : MonoBehaviour
{
    public string solution;
    public UnityEvent AddToSolution;
    public UnityEvent SubtractFromSolution;
    public UnityEvent OnDropItem;
}
