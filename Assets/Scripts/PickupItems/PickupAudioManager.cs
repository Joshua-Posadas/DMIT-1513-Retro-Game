using UnityEngine;

public class PickupAudioManager : MonoBehaviour
{
    public static PickupAudioManager Instance;

    public AudioClip pickupSound;
    private AudioSource source;

    private void Awake()
    {
        Instance = this;
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
    }

    public void PlayPickupSound()
    {
        if (pickupSound != null)
            source.PlayOneShot(pickupSound);
    }
}
