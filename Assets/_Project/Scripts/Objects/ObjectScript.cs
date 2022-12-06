using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    void Start()
    {
        GameManager.current.OnPlayerEnter += movingPlayer;
    }

    private void movingPlayer()
    {
        Debug.Log("Working Trigger");
    }
}
