using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentTransform;
    public string solution;
    Image image;

    private PuzzleSlot parentPuzzleSlot;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        //if (image.sprite == null) image.enabled = false;
        //else image.enabled = true;

        if (parentPuzzleSlot == null) return;
    }

    public void OnBeginDrag(PointerEventData eventData)
    { 
        parentTransform = transform.parent;
        //refers to the Canvas
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;

        if (parentPuzzleSlot == null) return;
        
        if (parentPuzzleSlot.solution == solution) parentPuzzleSlot.SubtractFromSolution.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        parentPuzzleSlot = parentTransform.GetComponent<PuzzleSlot>();
        transform.SetParent(parentTransform);
        image.raycastTarget = true;

        if (parentPuzzleSlot == null) return;
        if (parentPuzzleSlot.solution == solution)
        {
            parentPuzzleSlot.AddToSolution.Invoke();
        }
    }
}