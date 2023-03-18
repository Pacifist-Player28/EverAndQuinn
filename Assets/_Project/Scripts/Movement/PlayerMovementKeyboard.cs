using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementKeyboard : MonoBehaviour
{
    public static PlayerMovementKeyboard instance;
    [SerializeField] private KeyCode w;
    [SerializeField] private KeyCode a;
    [SerializeField] private KeyCode s;
    [SerializeField] private KeyCode d;

    [SerializeField] private KeyCode shift;

    [SerializeField] private KeyCode escape;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseBG;
    [SerializeField] private GameObject audioSettings;

    [SerializeField] private float speed = 3.25f;

    public KeyCode open;
    [SerializeField] private UnityEvent openInventory;
    [SerializeField] private UnityEvent closeInventory;
    [SerializeField] private UnityEvent clickedEscape;

    [SerializeField] private string walkLeft;
    [SerializeField] private string walkRight;
    [SerializeField] private string walkUp;
    [SerializeField] private string walkDown;
    public string idleFront;

    [HideInInspector] public Animator animator;
    private Rigidbody2D rb;
    private Vector2 vectorAnimation;
    private bool pauseActive;
    private bool movementEnabled = true;

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
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (movementEnabled)
        {
            Vector3 dir = new Vector3(0, 0, 0);

            if (Input.GetKey(w))
            {
                dir.y += 1;
            }
            if (Input.GetKey(a))
            {
                dir.x += -1;
            }
            if (Input.GetKey(s))
            {
                dir.y += -1;
            }
            if (Input.GetKey(d))
            {
                dir.x += 1;
            }

            transform.position += dir.normalized * speed * Time.deltaTime;

            vectorAnimation = dir;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(escape))
        {
            _TogglePause();
            clickedEscape.Invoke();
        }

        if (Input.GetKeyDown(shift))
        {
            speed = 10;
        }
        if (Input.GetKeyUp(shift))
        {
            speed = 5;
        }

        if (Input.GetKeyDown(open))
        {
            openInventory.Invoke();
        }
        if (Input.GetKeyUp(open))
        {
            closeInventory.Invoke();
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

    public void _TogglePause()
    {
        pauseActive = !pauseActive;
        pauseMenu.SetActive(pauseActive);
        pauseBG.SetActive(pauseActive);
        audioSettings.SetActive(false);

        movementEnabled = !pauseActive;
        if (!movementEnabled)
        {
            //GameSettings.instance._DialoguesActive(false);
            rb.velocity = Vector2.zero;
        }
        else
        {
            //GameSettings.instance._DialoguesActive(true);
            rb.velocity = vectorAnimation.normalized * speed;
        }

        Debug.Log("movement = " + movementEnabled);
    }
}
