using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    public string solution;
    public Image image;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        //refers to the Canvas
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        //cant place items into slots anymore????
        //if (image.sprite == null) image.raycastTarget = false;
        //else image.raycastTarget = true;

        if (image.sprite == null) image.enabled = false;
        else image.enabled = true;
    }
}
