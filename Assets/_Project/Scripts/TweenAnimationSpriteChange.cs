using UnityEngine;

public class TweenAnimationSpriteChange : MonoBehaviour
{
    [SerializeField] Transform startTransform;
    [SerializeField] Transform targetTransform;
    [SerializeField] float animationTime = 0.25f;
    [SerializeField] float minDistance;
    [SerializeField] Sprite one;
    [SerializeField] Sprite two;
    bool transformCheck = false;

    //if working with size, put down the size of the sprite down to 0 0 0, so it won't instatiate as a full sprite with full scale.
    private void Update()
    {
        //Debug.Log("Distance: " + Vector2.Distance(PlayerMovementKeyboard.instance.transform.position, transform.GetComponentInParent<Transform>().position));
        //Debug.Log("parent: " + transform.parent.name);
        if (Vector2.Distance(PlayerMovementKeyboard.instance.transform.position, transform.parent.position) <= minDistance)
        {
            //Debug.Log("DISTANCE SHORT!");
            MoveComponentUp();
        }
        else MoveComponentDown();
    }

    private void MoveComponentUp()
    {
        if (!transformCheck) 
        {
            //Debug.Log("UP");
            LeanTween.moveLocalY(gameObject, targetTransform.localPosition.y, animationTime).setEaseInOutQuad();
            LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), animationTime);
            transformCheck = true;
        }
        
    }

    private void MoveComponentDown()
    {
        if (transformCheck)
        {
            //Debug.Log("DOWN");
            LeanTween.moveLocalY(gameObject, startTransform.localPosition.y, animationTime).setEaseInOutQuad();
            LeanTween.scale(gameObject, new Vector3(0f, 0f, 0f), animationTime);
            transformCheck = false;
        }
    }

    public void ChangeSpriteStart()
    {
        GetComponent<SpriteRenderer>().sprite = one;
    }

    public void ChangeSpriteStop()
    {
        GetComponent<SpriteRenderer>().sprite = two;
    }
}
