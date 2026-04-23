using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class EndScreenController : MonoBehaviour
{
    public GameObject endScreenUI;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI killsText;

    public void ShowEndScreen(float timeSeconds, int kills)
    {
        Time.timeScale = 0f;

        timeText.text = Format(timeSeconds);
        killsText.text = kills + "/34";

        endScreenUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        PlayerStats.IsDead = true;

        var input = FindFirstObjectByType<PlayerInput>();
        if (input != null)
            input.enabled = false;
    }

    public void ReturnToMenu()
    {
        PlayerStats.IsDead = false;
        PauseController.IsPaused = false;

        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    private string Format(float t)
    {
        int m = Mathf.FloorToInt(t / 60f);
        int s = Mathf.FloorToInt(t % 60f);
        return m + ":" + s.ToString("00");
    }
}
