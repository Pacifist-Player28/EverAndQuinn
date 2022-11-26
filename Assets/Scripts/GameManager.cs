using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    public event Action onPlayerEnter;

    void Awake()
    {
        current = this;
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.Raycast(ray, Vector2.zero);

            if (rayHit)
            {
                //Debug.Log("We hit " + rayHit.collider.name);
            }
        }
    }

    //

    public void PlayerEnter()
    {
        if (onPlayerEnter != null)
        {
            onPlayerEnter();
        }
    }
}


