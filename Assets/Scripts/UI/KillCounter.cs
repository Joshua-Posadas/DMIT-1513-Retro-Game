using UnityEngine;

public class KillCounter : MonoBehaviour
{
    public static int Kills = 0;

    public static void ResetKills()
    {
        Kills = 0;
    }
}
