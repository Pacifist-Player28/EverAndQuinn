using UnityEngine;
using UnityEngine.UI;

public class TweenAnimation : MonoBehaviour
{
    [SerializeField] Transform startTransform;
    [SerializeField] Transform targetTransform;
    [SerializeField] float animationTime = 0.25f;
    [Space]
    [SerializeField] bool makeIconDissapear;
    [SerializeField] Image icon;

    private void Update()
    {
        if (Input.GetKeyDown(PlayerMovementKeyboard.instance.open))
        {
            MoveComponentUp();
        }

        if (Input.GetKeyUp(PlayerMovementKeyboard.instance.open))
        {
            MoveComponentDown();
        }
    }

    private void MoveComponentUp()
    {
        LeanTween.moveLocalY(gameObject, targetTransform.localPosition.y, animationTime).setEaseInOutQuad();
    }

    private void MoveComponentDown()
    {
        LeanTween.moveLocalY(gameObject, startTransform.localPosition.y, animationTime).setEaseInOutQuad();
    }
}
