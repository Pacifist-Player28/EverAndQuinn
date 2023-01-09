using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject loadingProgress;

    public void QuitGame()
    {
        Application.Quit();
    }

    public async void LoadGameWithLoadscreen(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            // display a loading screen or progress bar to the user
            Instantiate(loadingProgress);
            Debug.Log("Loading...");
        }
    }

    public void LoadGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}