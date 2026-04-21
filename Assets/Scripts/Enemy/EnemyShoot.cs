using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    public float fireRate = 1.0f;
    public float fireRange = 20f;
    public float shootDistance = 15f;
    public int damage = 10;

    private float fireTimer = 0f;

    [Header("Accuracy")]
    public float spreadAngle = 5f;

    [Header("Reload Settings")]
    public int shotsBeforeReload = 50;
    public float reloadTime = 2.0f;
    private int currentShots = 0;
    private bool isReloading = false;

    [Header("References")]
    public Transform firePoint;
    public Light muzzleFlashLight;
    public SpriteRenderer muzzleFlash;
    public float flashDuration = 0.05f;

    private PlayerStats playerStats;
    private Enemy enemy;
    private EnemyAudio enemyAudio;
    private DirectionalSpriteController spriteController;

    private void Start()
    {
        playerStats = Object.FindFirstObjectByType<PlayerStats>();
        enemy = GetComponent<Enemy>();
        enemyAudio = GetComponent<EnemyAudio>();
        spriteController = GetComponent<DirectionalSpriteController>();

        if (muzzleFlash != null)
            muzzleFlash.enabled = false;

        if (muzzleFlashLight != null)
            muzzleFlashLight.enabled = false;
    }

    private void Update()
    {
        if (enemy == null || enemy.IsDead())
            return;

        if (!enemy.CanSeePlayer())
            return;

        float dist = Vector3.Distance(transform.position, playerStats.transform.position);
        if (dist > shootDistance)
            return;

        if (isReloading)
            return;

        if (spriteController != null && !spriteController.IsFacingFront())
            return;

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            TryShoot();
            fireTimer = 0f;
        }
    }

    private void TryShoot()
    {
        if (firePoint == null)
            return;

        Vector3 direction = (playerStats.transform.position - firePoint.position).normalized;
        direction = ApplySpread(direction);

        if (Physics.Raycast(firePoint.position, direction, out RaycastHit hit, fireRange))
        {
            if (!hit.collider.CompareTag("Player"))
                return;

            playerStats.TakeDamage(damage);
        }

        if (enemyAudio != null)
            enemyAudio.PlayShoot();

        StartCoroutine(FlashMuzzle());

        currentShots++;

        if (currentShots >= shotsBeforeReload)
            StartCoroutine(Reload());
    }

    private Vector3 ApplySpread(Vector3 direction)
    {
        float angleX = Random.Range(-spreadAngle, spreadAngle);
        float angleY = Random.Range(-spreadAngle, spreadAngle);

        Quaternion rot = Quaternion.Euler(angleX, angleY, 0);
        return rot * direction;
    }

    private System.Collections.IEnumerator FlashMuzzle()
    {
        if (muzzleFlash != null)
            muzzleFlash.enabled = true;

        if (muzzleFlashLight != null)
            muzzleFlashLight.enabled = true;

        yield return new WaitForSeconds(flashDuration);

        if (muzzleFlash != null)
            muzzleFlash.enabled = false;

        if (muzzleFlashLight != null)
            muzzleFlashLight.enabled = false;
    }

    private System.Collections.IEnumerator Reload()
    {
        isReloading = true;

        if (enemyAudio != null)
            enemyAudio.PlayReload();

        yield return new WaitForSeconds(reloadTime);

        currentShots = 0;
        isReloading = false;
    }
}
