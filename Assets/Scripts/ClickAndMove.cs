using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndMove : MonoBehaviour
{
    public void Update()
    {
        OnMouseHover();
    }
    void OnMouseHover()
    {
        if (Input.GetMouseButtonDown(0)) print("Mouse Clicked!");
    }
}
