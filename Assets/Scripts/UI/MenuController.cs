using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Scene Names")]
    public string introSceneName = "IntroScene";
    public string gameSceneName = "Main Game";
    public string creditsSceneName = "Credits";

    public void PlayGame()
    {
        SceneManager.LoadScene(introSceneName);
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene(creditsSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
