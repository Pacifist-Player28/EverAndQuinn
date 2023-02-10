using UnityEngine;

public class TweenAnimationArrow : MonoBehaviour
{
    GameObject arrow;
    [SerializeField] float hoverAmount = 10.0f;
    [SerializeField] float hoverDuration = 1.0f;

    private void Start()
    {
        arrow = gameObject;
        LeanTween.moveY(arrow, arrow.transform.position.y + hoverAmount, hoverDuration).setLoopPingPong().setEaseInOutSine();
    }
}