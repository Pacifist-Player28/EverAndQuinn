using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] bool changeSprites;
    [Space]
    [SerializeField] Sprite hoverSprite, exitSprite;
    RectTransform children;
    Image thisImage;
    bool taken;

    private void Start()
    {
        thisImage = GetComponent<Image>();
        children = GetComponentInChildren<RectTransform>();
    }

    private void Update()
    {
        if (children == null) taken = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0) 
        { 
            GameObject dropped = eventData.pointerDrag;
            DragableItem dragableItem = dropped.GetComponent<DragableItem>();
            dragableItem.parentTransform = transform;
            taken = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(changeSprites && !taken)
            thisImage.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (changeSprites && !taken)
            thisImage.sprite = exitSprite;
    }
}
