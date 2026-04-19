using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 32;

    private void OnTriggerEnter(Collider other)
    {
        RaycastGun gun = other.GetComponentInChildren<RaycastGun>();

        if (gun != null)
        {
            gun.currentAmmo = gun.magazineSize;
            Destroy(gameObject);
        }
    }
}