using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [HideInInspector]
    public static MenuManager instance;
    [SerializeField]
    private Texture2D cursorTexture;
    [SerializeField]
    Slider volumeSlider;
    [SerializeField] AudioListener listener;

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
        //listener = FindObjectOfType<AudioListener>();
    }

    private void Update()
    {
        if (listener == null && volumeSlider == null) return;
        AudioListener.volume = (float)volumeSlider.value;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }


    //public void PauseGame()
    //{
    //    Time.timeScale = 0f;
    //}

    //public void ResumeGame()
    //{
    //    Time.timeScale = 1f;
    //}

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