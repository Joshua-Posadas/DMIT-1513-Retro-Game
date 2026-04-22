using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 32;

    private void OnTriggerEnter(Collider other)
    {
        RaycastGun gun = other.GetComponentInChildren<RaycastGun>();

        if (gun == null)
            return;

        if (gun.currentAmmo >= gun.magazineSize)
            return;

        gun.currentAmmo = Mathf.Clamp(gun.currentAmmo + ammoAmount, 0, gun.magazineSize);

        gun.PlayReloadAnimation();

        Destroy(gameObject);
    }
}
