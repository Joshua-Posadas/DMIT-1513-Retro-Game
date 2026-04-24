using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public AudioClip[] deathSounds;
    public AudioClip[] callouts;

    public AudioClip shootSound;
    public AudioClip reloadSound;

    private AudioSource source;

    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
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

    public void PlayShoot()
    {
        if (shootSound == null)
            return;

        source.pitch = 1f;
        source.PlayOneShot(shootSound);
    }

    public void PlayReload()
    {
        if (reloadSound == null)
            return;

        source.pitch = 1f;
        source.PlayOneShot(reloadSound);
    }
}
