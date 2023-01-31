using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [HideInInspector]
    public static MenuManager instance;
    [SerializeField]
    private Texture2D cursorTexture;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

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

    //public void loadgame(string scenename)
    //{
    //    scenemanager.loadscene(scenename);
    //}
}