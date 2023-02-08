using Inventory;
using UnityEngine;
using UnityEngine.Events;

public class ObjectTriggerScript : MonoBehaviour
{
    bool canBeClicked = false;
    bool clickedCheck = false;
    GameObject player;
    [SerializeField] float triggerDistance = 7.5f;
    [SerializeField] UnityEvent clicked;
    [SerializeField] UnityEvent clickStop;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        OnDistanceShorter(triggerDistance);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GameSettings.current.PlayerEnter();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && canBeClicked && !clickedCheck)
        {
            clicked.Invoke();
            clickedCheck = true;
        }
        
        if(Input.GetMouseButtonDown(0) && canBeClicked && clickedCheck)
        {
            clickStop.Invoke();
            clickedCheck = false;
        }
    }

    public void OnDistanceShorter(float distance)
    {
        if (Vector2.Distance(GetComponent<Transform>().position, player.transform.position) < distance)
        {
            //Debug.Log("Distance is short enough");
            canBeClicked = true;
        }
        else canBeClicked = false;
    }

    //public void DeactivateItems(InventoryUI inventory, string layerNameToDeactivate)
    //{
    //    for (int i = 0; i < inventory.slots.Length; i++)
    //    {
    //        if (inventory.slots[i].transform.GetChild(0).GetComponent<DragableItem>().)
    //    }
    //}
}
