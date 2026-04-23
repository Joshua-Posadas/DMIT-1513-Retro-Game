using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public string mainMenuSceneName = "Main Menu";

    public void BackToMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
