using UnityEngine;

public class UziPickup : MonoBehaviour
{
    public GameObject uziRight;
    public GameObject ammoUI;
    public AudioSource audioSource;
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        PlayerWeapons weapons = other.GetComponent<PlayerWeapons>();
        if (weapons == null)
            return;

        if (weapons.hasUzi)
            return;

        weapons.hasUzi = true;

        if (uziRight != null)
            uziRight.SetActive(true);

        if (ammoUI != null)
            ammoUI.SetActive(true);

        if (audioSource != null && pickupSound != null)
            audioSource.PlayOneShot(pickupSound);

        Destroy(gameObject);
    }
}
