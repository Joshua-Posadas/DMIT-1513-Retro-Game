using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IDamagable
{
    [Header("Health")]
    public float maxHealth = 50f;
    private float currentHealth;

    [Header("Sprite Controller")]
    public DirectionalSpriteController spriteController;

    [Header("Sprites")]
    public Sprite frontInjured;
    public Sprite corpseFinal;

    private bool isInjured = false;
    private bool isDead = false;
    private bool corpseActive = false;
    private bool hasCalledOut = false;

    [Header("Detection Settings")]
    public Transform player;
    public float viewDistance = 10f;
    public float viewAngle = 60f;
    public float turnSpeed = 3f;

    private EnemyAudio audioSource;

    private void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<EnemyAudio>();
    }

    private void Update()
    {
        if (!isDead)
        {
            DetectPlayer();
            RotateTowardPlayer();
        }
    }

    private void OnSeePlayer()
    {
        if (hasCalledOut)
            return;

        if (Random.value <= 0.75f && audioSource != null)
            audioSource.PlayCallout();

        hasCalledOut = true;
    }

    private void DetectPlayer()
    {
        if (hasCalledOut)
            return;

        Vector3 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;

        if (distance > viewDistance)
            return;

        float angle = Vector3.Angle(transform.forward, toPlayer.normalized);
        if (angle > viewAngle * 0.5f)
            return;

        OnSeePlayer();
    }

    private void RotateTowardPlayer()
    {
        Vector3 direction = (player.position - transform.position);
        direction.y = 0;

        if (direction.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);
    }

    public void TakeDamage(float amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;

        if (!isInjured && currentHealth <= maxHealth * 0.25f)
        {
            isInjured = true;
            spriteController.SetFrontSprite(frontInjured);
        }

        if (currentHealth <= 0)
        {
            StartCoroutine(DeathSequence());
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public bool CanSeePlayer()
    {
        return hasCalledOut && !isDead;
    }

    private IEnumerator DeathSequence()
    {
        isDead = true;
        if (audioSource != null)
            audioSource.PlayDeathSound();

        spriteController.EnterCorpseMode();

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var s in scripts)
        {
            if (s != this && s != spriteController)
                s.enabled = false;
        }

        float shakeDuration = 2f;
        float shakeSpeed = 10f;
        float shakeAmount = 0.02f;

        float timer = 0f;
        Vector3 originalPos = spriteController.spritePivot.localPosition;

        while (timer < shakeDuration)
        {
            float offset = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
            spriteController.spritePivot.localPosition = originalPos + new Vector3(offset, 0, 0);

            timer += Time.deltaTime;
            yield return null;
        }

        spriteController.spritePivot.localPosition = originalPos;
        spriteController.ForceSprite(corpseFinal);

        Collider trigger = gameObject.AddComponent<BoxCollider>();
        trigger.isTrigger = true;

        corpseActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!corpseActive)
            return;

        RaycastGun gun = other.GetComponentInChildren<RaycastGun>();
        if (gun != null)
        {
            gun.currentAmmo = Mathf.Clamp(gun.currentAmmo + 16, 0, gun.magazineSize);
            gun.PlayReloadAnimation();

            Collider trigger = GetComponent<Collider>();
            if (trigger != null)
                trigger.enabled = false;

            corpseActive = false;
        }
    }
}
