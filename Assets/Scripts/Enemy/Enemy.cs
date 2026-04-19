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

    private void Start()
    {
        currentHealth = maxHealth;
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

    private IEnumerator DeathSequence()
    {
        isDead = true;

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
            gun.currentAmmo = gun.magazineSize;
            gun.PlayReloadAnimation();

            Collider trigger = GetComponent<Collider>();
            if (trigger != null)
                trigger.enabled = false;

            corpseActive = false;
        }
    }
}
