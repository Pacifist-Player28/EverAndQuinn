using UnityEngine;
using UnityEngine.Events;

public class ObjectTriggerScript : MonoBehaviour
{
    bool canBeClicked = false;
    GameObject player;
    [SerializeField] float triggerDistance = 7.5f;
    [SerializeField] UnityEvent clicked;

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
        if (Input.GetMouseButtonDown(0) && canBeClicked)
        {
            //Debug.Log("clicked");
            clicked.Invoke();
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
}
