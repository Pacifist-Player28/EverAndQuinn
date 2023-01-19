using UnityEngine;
using UnityEngine.UI;

public class BlockRay : MonoBehaviour
{
    public Image image;

    private GameObject[] interactables;
    private Ray raycast;

    private void Start()
    {
        raycast = FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition);

        interactables = GameObject.FindGameObjectsWithTag("Interactable");
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycast, out hit))
        {
            if (hit.transform == image.transform) 
            { 
                Debug.Log("JAHSLFKWEIUFVUIEWF");

                for (int i = 0; i < interactables.Length; i++)
                {
                interactables[i].GetComponent<Collider2D>().enabled = false;
                }
            }

            else
            {
                Debug.Log("alkfkadjkj");
                for (int i = 0; i < interactables.Length; i++)
                {
                    interactables[i].GetComponent<Collider2D>().enabled = true;
                }
            }
        }

    }
}
