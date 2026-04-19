using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastGun : MonoBehaviour
{
    [Header("Gun Settings")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 0.1f;

    [Header("Ammo")]
    public int magazineSize = 32;
    public int currentAmmo;

    [Header("Reload Animation")]
    public float reloadDuration = 0.5f;
    public Vector3 loweredPosition = new Vector3(0, -0.3f, 0);
    public AudioClip reloadSound;
    public AudioSource audioSource;

    [Header("References")]
    public Camera fpsCamera;
    public Recoil cameraRecoil;
    public WeaponKickback weaponKick;
    public MuzzleFlashLight lightFlash;
    public MuzzleFlashIMG imgFlash;
    public WeaponAudio weaponAudio;

    private float nextFireTime;
    private bool isReloading = false;
    private Vector3 originalLocalPos;

    private void Start()
    {
        currentAmmo = magazineSize;
        originalLocalPos = transform.localPosition;
    }

    private void Update()
    {
        if (Mouse.current == null)
            return;

        if (isReloading)
            return;

        if (currentAmmo <= 0)
            return;

        if (Mouse.current.leftButton.isPressed && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
            currentAmmo--;
        }
    }

    private void Shoot()
    {
        if (cameraRecoil != null)
            cameraRecoil.ApplyRecoil();

        if (weaponKick != null)
            weaponKick.ApplyKick();

        if (lightFlash != null)
            lightFlash.TriggerFlash();

        if (imgFlash != null)
            imgFlash.TriggerFlash();

        if (weaponAudio != null)
            weaponAudio.PlayFire();

        Ray ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            IDamagable target = hit.collider.GetComponent<IDamagable>();
            if (target != null)
                target.TakeDamage(damage);
        }
    }

    public void PlayReloadAnimation()
    {
        if (!isReloading)
            StartCoroutine(ReloadAnimationRoutine());
    }

    private System.Collections.IEnumerator ReloadAnimationRoutine()
    {
        isReloading = true;

        if (audioSource != null && reloadSound != null)
            audioSource.PlayOneShot(reloadSound);

        float t = 0f;
        while (t < reloadDuration)
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(originalLocalPos, loweredPosition, t / reloadDuration);
            yield return null;
        }

        t = 0f;
        while (t < reloadDuration)
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(loweredPosition, originalLocalPos, t / reloadDuration);
            yield return null;
        }

        transform.localPosition = originalLocalPos;
        isReloading = false;
    }
}
