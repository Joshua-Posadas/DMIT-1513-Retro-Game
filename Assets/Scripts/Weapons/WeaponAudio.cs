using UnityEngine;

public class WeaponAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip fireSound;

    public void PlayFire()
    {
        if (audioSource != null && fireSound != null)
            audioSource.PlayOneShot(fireSound);
    }
}
