using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip[] deathSounds;
    public AudioClip[] callouts;

    private AudioSource source;

    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 1f;
    }

    public void PlayCallout()
    {
        if (callouts.Length == 0)
            return;

        int index = Random.Range(0, callouts.Length);
        source.PlayOneShot(callouts[index]);
    }

    public void PlayDeathSound()
    {
        if (deathSounds.Length == 0)
            return;

        source.Stop();

        int index = Random.Range(0, deathSounds.Length);
        source.PlayOneShot(deathSounds[index]);
    }
}
