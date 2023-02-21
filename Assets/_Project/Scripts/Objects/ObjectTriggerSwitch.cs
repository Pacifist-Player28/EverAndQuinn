using UnityEngine;
using UnityEngine.Events;

public class ObjectTriggerSwitch : MonoBehaviour
{
    bool canBeClicked = false;
    bool clickedCheck = false;

    [SerializeField] Sprite spriteOff;
    [SerializeField] Sprite spriteOn;
    [Space]
    [SerializeField] float triggerDistance = 7.5f;
    [SerializeField] UnityEvent clicked;
    [SerializeField] UnityEvent clickStop;

    GameObject player;

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
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && canBeClicked && !clickedCheck)
        {
            clicked.Invoke();
            clickedCheck = true;
            //Debug.Log("clicked ON");
            if (spriteOn == null) return;
            else GetComponent<SpriteRenderer>().sprite = spriteOn;
        }
        else if (Input.GetMouseButtonDown(0) && canBeClicked && clickedCheck)
        {
            clickStop.Invoke();
            clickedCheck = false;
            //Debug.Log("clicked OFF");
            if (spriteOn == null) return;
            else GetComponent<SpriteRenderer>().sprite = spriteOff;
        }
    }

    public void OnDistanceShorter(float distance)
    {
        if (Vector2.Distance(GetComponent<Transform>().position, player.transform.position) < distance)
        {
            canBeClicked = true;
        }
        else canBeClicked = false;
    }
}