using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    public string solution;
    public Image image;

    private PuzzleSlot parentPuzzleSlot;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (image.sprite == null) image.enabled = false;
        else image.enabled = true;

        if (parentPuzzleSlot == null) return;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //parentPuzzleSlot = parentAfterDrag.GetComponent<PuzzleSlot>();  
        parentAfterDrag = transform.parent;
        //refers to the Canvas
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;

        if (parentPuzzleSlot == null) return;
        if (parentPuzzleSlot.solution == solution)
        {
            parentPuzzleSlot.SubtractFromSolution.Invoke();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        parentPuzzleSlot = parentAfterDrag.GetComponent<PuzzleSlot>();
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;

        if (parentPuzzleSlot == null) return;
        if (parentPuzzleSlot.solution == solution)
        {
            parentPuzzleSlot.AddToSolution.Invoke();
        }
    }


}