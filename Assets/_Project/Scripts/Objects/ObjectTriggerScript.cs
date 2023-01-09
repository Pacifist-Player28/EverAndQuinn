using UnityEngine;
using UnityEngine.Events;

public class ObjectTriggerScript : MonoBehaviour
{
    private bool canBeClicked = false;
    public float triggerDistance = 7.5f;
    public UnityEvent clicked;

    private GameObject player;

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
        Debug.Log("Hovering");
        if (Input.GetMouseButton(0) && canBeClicked)
        {
            Debug.Log("clicked");
            clicked.Invoke();
        }
    }

    public void OnDistanceShorter(float distance)
    {
        if (Vector2.Distance(GetComponent<Transform>().position, player.transform.position) < distance)
        {
            Debug.Log("Distance is short enough");
            canBeClicked = true;
        }
        else canBeClicked = false;
    }
}
