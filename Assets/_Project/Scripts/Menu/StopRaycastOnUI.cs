using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StopRaycastOnUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Camera mainCamera;
    private GraphicRaycaster canvasRaycaster;

    private void Start()
    {
        // Get main camera and graphic raycaster
        mainCamera = Camera.main;
        canvasRaycaster = GetComponentInParent<GraphicRaycaster>();

        // Disable canvas raycasting by default
        canvasRaycaster.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Disable main camera's raycasting and enable canvas raycasting
        mainCamera.GetComponent<PhysicsRaycaster>().enabled = false;
        canvasRaycaster.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Enable main camera's raycasting and disable canvas raycasting
        mainCamera.GetComponent<PhysicsRaycaster>().enabled = true;
        canvasRaycaster.enabled = false;
    }
}
