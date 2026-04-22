using UnityEngine;

public class DualUziPickup : MonoBehaviour
{
    public GameObject uziLeft;
    public RaycastGun rightGun;
    public AudioSource audioSource;
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        PlayerWeapons weapons = other.GetComponent<PlayerWeapons>();
        if (weapons == null)
            return;

        if (!weapons.hasUzi)
            return;

        if (weapons.hasDualUzi)
            return;

        weapons.hasDualUzi = true;

        if (uziLeft != null)
            uziLeft.SetActive(true);

        rightGun.magazineSize = 128;
        rightGun.currentAmmo = Mathf.Clamp(rightGun.currentAmmo, 0, 128);

        if (audioSource != null && pickupSound != null)
            audioSource.PlayOneShot(pickupSound);

        Destroy(gameObject);
    }
}
