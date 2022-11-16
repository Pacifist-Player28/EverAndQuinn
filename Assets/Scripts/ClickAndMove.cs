using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndMove : MonoBehaviour
{
    public Vector3 targetPosition = new Vector3 (0f, 0f, 0f);
    public Transform player;
    public int speed = 1;

//Update und Start
    public void Start()
    {

    }

    public void Update()
    {
        OnMouseClick();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        }
        player.position = Vector3.MoveTowards(player.position, targetPosition, Time.deltaTime * speed);
    }

    public void FixedUpdate()
    {
        
    }

//Events

    void OnMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) print("Mouse Clicked!");
    }
}
