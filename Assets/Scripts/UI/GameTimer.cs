using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static float TimeElapsed = 0f;

    private void Update()
    {
        if (!PauseController.IsPaused && !PlayerStats.IsDead)
            TimeElapsed += Time.deltaTime;
    }

    public static void ResetTimer()
    {
        TimeElapsed = 0f;
    }
}
