using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform playerPos;

    private void Start()
    {
        playerPos = FindObjectOfType<PlayerMovementKeyboard>().transform;
    }

    private void Update()
    {
        transform.position = playerPos.position + new Vector3(0f, 0f, -10f);
    }
}
