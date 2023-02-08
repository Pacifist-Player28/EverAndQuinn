using Inventory;
using UnityEngine;
using UnityEngine.Events;

public class ObjectTriggerScript : MonoBehaviour
{
    bool canBeClicked = false;
    bool clickedCheck = false;
    GameObject player;
    InventoryManager im;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
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
            Debug.Log("clicked ON");
        }
        else if(Input.GetMouseButtonDown(0) && canBeClicked && clickedCheck)
        {
            clickStop.Invoke();
            clickedCheck = false;
            Debug.Log("clicked OFF");
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

    public void DeactivateItems(int layerToDeactivate)
    {
        for (int i = 0; i < InventoryUI.instance.slots.Length; i++)
        {
            if (InventoryUI.instance.slots[i].layer == layerToDeactivate)   
                InventoryUI.instance.slots[i].SetActive(false);
        }
    }

    public void ActivateItems()
    {
        for (int i = 0; i < InventoryUI.instance.slots.Length; i++)
        {
            if (InventoryUI.instance.slots[i].activeSelf == false)
                InventoryUI.instance.slots[i].SetActive(true);
        }
    }
}
