using UnityEngine;
using UnityEngine.UI;

public class BlockRay : MonoBehaviour
{

    public Image image;

    void Update()
    {
        if (image.raycastTarget)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject == image.gameObject)
            {
                Debug.Log("YUHU TARGET FOUND");
                return;
            }
        }
    }
}
