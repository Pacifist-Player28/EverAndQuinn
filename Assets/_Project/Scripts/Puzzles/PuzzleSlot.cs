using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Inventory;

public class PuzzleSlot : MonoBehaviour, IDropHandler, IDragHandler, IBeginDragHandler
{
    public string solution;
    public UnityEvent AddToSolution;
    public UnityEvent SubtractFromSolution;
    public UnityEvent OnDropItem;

    private GameObject dropped;

    public void OnDrop(PointerEventData eventData)
    {
        //dropped = eventData.pointerDrag;
        //DragableItem dragableItem = dropped.GetComponent<DragableItem>();
        //if (transform.childCount == 0)
        //{
        //    dragableItem.parentAfterDrag = transform;
        //    OnDropItem.Invoke();
        //}

        //if (dragableItem.solution == solution)
        //{
        //    Debug.Log("Solution puzzle " + dropped.tag);
        //    AddToSolution.Invoke();
        //}
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //dropped = GetComponentInChildren<Transform>().gameObject;
        //DragableItem dragableItem = dropped.GetComponent<DragableItem>();

        //if (dragableItem.solution == solution)
        //{
        //    Debug.Log("Solution puzzle " + dropped.tag);
        //    AddToSolution.Invoke();
        //}
    }
}
