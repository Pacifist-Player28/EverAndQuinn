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

    [Header("Animations")]
    public string walkLeft;
    public string walkRight;
    public string walkUp;
    public string walkDown;
    [Space]
    public string idleFront;

    private Animator animator;
    private new Vector2 vectorAnimation;

    //

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        Vector3 dir = new Vector3(0, 0, 0);

        if (Input.GetKey(W)) dir.y += 1;
        if (Input.GetKey(A)) dir.x += -1;
        if (Input.GetKey(S)) dir.y += -1;
        if (Input.GetKey(D)) dir.x += 1;

        transform.position += dir.normalized * speed * Time.deltaTime;

        vectorAnimation = dir;
    }

    public void Update()
    {
        //delete both if statements on build!!!
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

        if (vectorAnimation.x == 0f && vectorAnimation.y == 0f)
        {
            animator.Play(idleFront);
        }

        if(vectorAnimation.x < 0)
        {
            animator.Play(walkLeft);
        }

        if (vectorAnimation.x > 0)
        {
            animator.Play(walkRight);
        }

        if (vectorAnimation.y < 0)
        {
            animator.Play(walkDown);
        }

        if (vectorAnimation.y > 0)
        {
            animator.Play(walkUp);
        }
    }
}
