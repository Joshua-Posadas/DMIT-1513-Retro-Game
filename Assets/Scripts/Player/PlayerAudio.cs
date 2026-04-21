using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip[] hurtSounds;
    public AudioClip[] armorHitSounds;
    public AudioClip[] deathSounds;

    private AudioSource source;

    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 1f;
    }

    public void PlayHurtSound()
    {
        if (hurtSounds.Length == 0)
            return;

        int index = Random.Range(0, hurtSounds.Length);
        source.PlayOneShot(hurtSounds[index]);
    }

    public void PlayArmorHit()
    {
        if (armorHitSounds.Length == 0)
            return;

        int index = Random.Range(0, armorHitSounds.Length);
        source.PlayOneShot(armorHitSounds[index]);
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
