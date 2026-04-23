using UnityEngine;

public class EndDoor : MonoBehaviour, IInteractable
{
    public EndScreenController endScreen;

    public void Interact()
    {
        endScreen.ShowEndScreen(GameTimer.TimeElapsed, KillCounter.Kills);
    }
}
