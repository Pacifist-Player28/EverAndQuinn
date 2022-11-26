using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTriggerScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        GameManager.current.PlayerEnter();
    }

    void OnMouseOver()
    {
        Debug.Log("Hovering");
        if (Input.GetMouseButtonDown(0))
        {
            this.enabled = false;
        }
    }
}
