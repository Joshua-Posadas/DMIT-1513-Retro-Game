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

    private void Start()
    {
        currentHealth = maxHealth;
        currentArmor = 0;

        defaultHealthColor = healthText.color;

        UpdateUI();
    }

    // Damage Test Note: REMOVE AFTER ENEMY AI IS DONE
    private void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int amount)
    {
        if (currentArmor > 0)
        {
            int absorbed = Mathf.Min(currentArmor, amount);
            currentArmor -= absorbed;
            amount -= absorbed;
        }

        if (amount > 0)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (damageFlash != null)
                damageFlash.TriggerFlash();
        }

        UpdateUI();
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
