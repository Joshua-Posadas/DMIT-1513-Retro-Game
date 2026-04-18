using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastGun : MonoBehaviour
{
    [Header("Gun Settings")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 0.1f;

    [Header("References")]
    public Camera fpsCamera;
    public Recoil cameraRecoil;
    public WeaponKickback weaponKick;

    private float nextFireTime;

    private void Update()
    {
        if (Mouse.current == null)
            return;

        if (Mouse.current.leftButton.isPressed && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (cameraRecoil != null)
            cameraRecoil.ApplyRecoil();

        if (weaponKick != null)
            weaponKick.ApplyKick();

        Ray ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            IDamagable target = hit.collider.GetComponent<IDamagable>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            // Temporary Debug
            Debug.Log("Hit: " + hit.collider.name);
        }
    }
}
