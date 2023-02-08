using UnityEngine;
using UnityEngine.UI;

public class TweenAnimationUi : MonoBehaviour
{
    [SerializeField] Transform startTransform;
    [SerializeField] Transform targetTransform;
    [SerializeField] float animationTime = 0.25f;
    [Space]
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

    public void MoveComponentUp()
    {
        LeanTween.moveLocalY(gameObject, targetTransform.localPosition.y, animationTime).setEaseInOutQuad();
        icon.rectTransform.rotation = new Quaternion(0f, 0f, 360f, 0f);
    }

    public void MoveComponentDown()
    {
        LeanTween.moveLocalY(gameObject, startTransform.localPosition.y, animationTime).setEaseInOutQuad();
        icon.rectTransform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }
}
