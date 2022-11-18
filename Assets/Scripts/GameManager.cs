using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    void Awake()
    {
        current = this;
    }

    public event Action onPlayerEnter;

    public void PlayerEnter()
    {
        if (onPlayerEnter != null)
        {
            onPlayerEnter();
        }
    }
}
