using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 100;
    public int currentHealth;

    public int maxArmor = 100;
    public int currentArmor;

    [Header("UI")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI armorText;
    public DamageFlash damageFlash;

    private Color defaultHealthColor;
    private bool isDead = false;

    private PlayerAudio playerAudio;

    private void Start()
    {
        currentHealth = maxHealth;
        currentArmor = 0;

        defaultHealthColor = healthText.color;

        playerAudio = GetComponent<PlayerAudio>();

        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
            return;

        int originalAmount = amount;

        if (currentArmor > 0)
        {
            int absorbed = Mathf.Min(currentArmor, amount);
            currentArmor -= absorbed;
            amount -= absorbed;

            if (absorbed > 0 && playerAudio != null)
                playerAudio.PlayArmorHit();
        }

        if (amount > 0)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (currentHealth > 0 && playerAudio != null)
                playerAudio.PlayHurtSound();

            if (damageFlash != null)
                damageFlash.TriggerFlash();
        }

        UpdateUI();
        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;
        if (playerAudio != null)
            playerAudio.PlayDeathSound();
    }

    public void UpdateUI()
    {
        healthText.text = currentHealth.ToString();
        armorText.text = currentArmor.ToString();

        if (currentHealth <= 25)
            healthText.color = Color.red;
        else
            healthText.color = defaultHealthColor;
    }
}
