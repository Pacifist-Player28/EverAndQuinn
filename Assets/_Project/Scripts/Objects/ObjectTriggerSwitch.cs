//use this script in order to start events and change the sprite when interacting with the object (for example lamps).
using UnityEngine;
using UnityEngine.Events;

public class ObjectTriggerSwitch : MonoBehaviour
{
    bool canBeClicked = false;
    bool clickedOn = false;

    [SerializeField] Sprite spriteOff;
    [SerializeField] Sprite spriteOn;
    [Space]
    [SerializeField] float triggerDistance = 7.5f;
    [SerializeField] UnityEvent clickOnce;
    [SerializeField] UnityEvent clickTwice;

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
        if (Input.GetMouseButtonDown(0) && canBeClicked)
        {
            clickedOn = !clickedOn;
            if (clickedOn)
            {
                clickOnce.Invoke();
                if (spriteOn != null)
                {
                    GetComponent<SpriteRenderer>().sprite = spriteOn;
                }
            }
            else
            {
                clickTwice.Invoke();
                if (spriteOff != null)
                {
                    GetComponent<SpriteRenderer>().sprite = spriteOff;
                }
            }
        }
    }


    public void OnDistanceShorter(float distance)
    {
        canBeClicked = Vector2.Distance(transform.position, player.transform.position) < distance;
    }

}