using UnityEngine;

public class FlashbackCollision : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string animationName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.Play(animationName);
        }
    }
}
