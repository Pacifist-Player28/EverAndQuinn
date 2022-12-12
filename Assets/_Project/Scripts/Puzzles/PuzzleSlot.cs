using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PuzzleSlot : MonoBehaviour, IDropHandler
{
    public string solution;
    public UnityEvent AddToSolution;

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0) 
        { 
        GameObject dropped = eventData.pointerDrag;
        DragableItem dragableItem = dropped.GetComponent<DragableItem>();
        dragableItem.parentAfterDrag = transform;
        }

        GameObject droppedItem = eventData.pointerDrag;
        if (droppedItem.GetComponent<DragableItem>().solution == solution)
        {
            Debug.Log("Solution puzzle " + droppedItem.tag);
            AddToSolution.Invoke();
        }
    }
}
