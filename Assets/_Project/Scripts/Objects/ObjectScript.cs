using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    void Start()
    {
        GameSettings.current.OnPlayerEnter += movingPlayer;
    }

    private void movingPlayer()
    {
        Debug.Log("Working Trigger");
    }
}
