using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementKeyboard : MonoBehaviour
{
    //public Rigidbody2D rb;

    [Header("Controls")]
    public KeyCode W;
    public KeyCode A;
    public KeyCode S;
    public KeyCode D;

    public KeyCode Shift;

    [Header("Speed")]
    public float speed;

    [Header("Open Inventory")]
    public KeyCode openInventory;
    public UnityEvent OpenInventory;
    public UnityEvent CloseInventory;

    //

    public void FixedUpdate()
    {
        Vector3 dir = new Vector3(0, 0, 0);

        if (Input.GetKey(W)) dir.y += 1;
        if (Input.GetKey(A)) dir.x += -1;
        if (Input.GetKey(S)) dir.y += -1;
        if (Input.GetKey(D)) dir.x += 1;

        transform.position += dir.normalized * speed * Time.deltaTime;


    }

    public void Update()
    {
        if (Input.GetKeyDown(Shift)) speed = 10;

        if (Input.GetKeyUp(Shift)) speed = 5;

        if (Input.GetKeyDown(openInventory))
        {
            OpenInventory.Invoke();
            Debug.Log("Open Inventory");
        }

        if (Input.GetKeyUp(openInventory))
        {
            CloseInventory.Invoke();
            Debug.Log("Close Inventory");
        }
    }
}
