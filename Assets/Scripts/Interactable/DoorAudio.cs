using UnityEngine;

public class DoorAudio : MonoBehaviour
{
    private AudioSource source;
    public AudioClip openSound;
    public AudioClip closeSound;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if (source == null)
            source = gameObject.AddComponent<AudioSource>();

        source.playOnAwake = false;
        source.spatialBlend = 1f;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.minDistance = 2f;
        source.maxDistance = 20f;
        source.dopplerLevel = 0f;
    }

    public void PlayOpen()
    {
        if (openSound != null)
            source.PlayOneShot(openSound);
    }

    public void PlayClose()
    {
        if (closeSound != null)
            source.PlayOneShot(closeSound);
    }
}
