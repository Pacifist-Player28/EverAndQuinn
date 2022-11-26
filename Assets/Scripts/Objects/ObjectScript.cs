using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    void Start()
    {
        GameManager.current.onPlayerEnter += movingPlayer;
    }

    private void movingPlayer()
    {
        Debug.Log("Working Trigger");
    }
}
