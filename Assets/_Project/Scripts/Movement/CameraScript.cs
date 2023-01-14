using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform playerPos;
    public Vector2 minCameraPos;
    public Vector2 maxCameraPos;

    private void Start()
    {
        playerPos = FindObjectOfType<PlayerMovementKeyboard>().transform;
    }

    private void Update()
    {
        float playerX = playerPos.position.x;
        float playerY = playerPos.position.y;

        playerX = Mathf.Clamp(playerX, minCameraPos.x, maxCameraPos.x);
        playerY = Mathf.Clamp(playerY, minCameraPos.y, maxCameraPos.y);

        transform.position = new Vector3(playerX, playerY, -10f);
    }
}
