using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public static bool IsPaused = false;

    private bool isPaused = false;
    private InputAction pauseAction = new InputAction(binding: "<Keyboard>/escape");

    void OnEnable()
    {
        pauseAction.performed += TogglePause;
        pauseAction.Enable();
    }

    void OnDisable()
    {
        pauseAction.Disable();
    }

    private void TogglePause(InputAction.CallbackContext ctx)
    {
        if (PlayerStats.IsDead)
            return;

        if (isPaused)
            Continue();
        else
            Pause();
    }

    public void Pause()
    {
        isPaused = true;
        IsPaused = true;

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Continue()
    {
        isPaused = false;
        IsPaused = false;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Return()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
