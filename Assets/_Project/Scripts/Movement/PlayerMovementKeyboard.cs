using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementKeyboard : MonoBehaviour
{
    [HideInInspector]
    public static PlayerMovementKeyboard instance;

    [Header("Controls")]
    [SerializeField] KeyCode W;
    [SerializeField] KeyCode A;
    [SerializeField] KeyCode S;
    [SerializeField] KeyCode D;
    [Space]
    [SerializeField] KeyCode Shift;
    [Space]
    [SerializeField] KeyCode Escape;

    [Header("Speed")]
    public float speed;
    //3.25

    [Header("Open Inventory")]
    public KeyCode open;
    [SerializeField] UnityEvent openInventory;
    [SerializeField] UnityEvent closeInventory;
    [SerializeField] UnityEvent clickedEscape;

    [Header("Animations")]
    [SerializeField] string walkLeft;
    [SerializeField] string walkRight;
    [SerializeField] string walkUp;
    [SerializeField] string walkDown;
    [Space]
    public string idleFront;
    [HideInInspector]
    public Animator animator;
    private new Vector2 vectorAnimation;

    //

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

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
        if (Input.GetKeyDown(Escape)) clickedEscape.Invoke();

        // delete both if statements on build!!!
        if (Input.GetKeyDown(Shift)) speed = 10;
        if (Input.GetKeyUp(Shift)) speed = 5;

        if (Input.GetKeyDown(open))
        {
            openInventory.Invoke();
            Debug.Log("Open Inventory");
        }

        if (Input.GetKeyUp(open))
        {
            closeInventory.Invoke();
            Debug.Log("Close Inventory");
        }

        if (vectorAnimation.x == 0f && vectorAnimation.y == 0f)
        {
            animator.Play(idleFront);
        }
        else
        {
            if (vectorAnimation.x < 0) animator.Play(walkLeft);
            else if (vectorAnimation.x > 0) animator.Play(walkRight);
            else if (vectorAnimation.y < 0) animator.Play(walkDown);
            else if (vectorAnimation.y > 0) animator.Play(walkUp);
        }
    }

}
