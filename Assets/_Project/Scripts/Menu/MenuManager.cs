using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingProgress;
    [SerializeField]
    private Texture2D cursorTexture;

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    //public async void LoadGameWithLoadscreen(string sceneName)
    //{
    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

    //    while (!asyncLoad.isDone)
    //    {
    //        // display a loading screen or progress bar to the user
    //        Instantiate(loadingProgress);
    //        Debug.Log("Loading...");
    //    }
    //}

    public void LoadGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}