using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPointclick : MonoBehaviour
{
    [Header("Speed")]
    public int speed = 1;

    [Header("Position")]
    public Vector2 targetPosition = new Vector2(0f, 0f);

    public void Update()
    {
        //move the player towards the last mouse click.
        if (Input.GetKeyDown(KeyCode.Mouse0)) targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, -10);
        this.transform.position = Vector2.MoveTowards(this.transform.position, targetPosition, Time.deltaTime * speed);

        //stops the player when colliding with an object.
        //if (triggerIn) player.transform.position = Vector2.MoveTowards(player.transform.position, player.transform.position, Time.deltaTime * speed);
    }
}
